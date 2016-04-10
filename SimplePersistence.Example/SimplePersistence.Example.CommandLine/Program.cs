#region License
// // The MIT License (MIT)
// // 
// // Copyright (c) 2016 SimplePersistence
// // 
// // Permission is hereby granted, free of charge, to any person obtaining a copy
// // of this software and associated documentation files (the "Software"), to deal
// // in the Software without restriction, including without limitation the rights
// // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// // copies of the Software, and to permit persons to whom the Software is
// // furnished to do so, subject to the following conditions:
// // 
// // The above copyright notice and this permission notice shall be included in all
// // copies or substantial portions of the Software.
// // 
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// // SOFTWARE.
#endregion

using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using NLog;
using SimpleInjector;
using SimplePersistence.Example.Models.Logging;
using SimplePersistence.Example.UoW;
using SimplePersistence.Example.UoW.IoC;
using SimplePersistence.UoW;
using SimplePersistence.UoW.Helper;

namespace SimplePersistence.Example.CommandLine
{
    public class Program
    {
        private static readonly ILogger Logger;
        private readonly IUnitOfWorkFactory _factory;

        #region Statics

        static Program()
        {
            GlobalDiagnosticsContext.Set(
                "assemblyVersion",
                Assembly.GetExecutingAssembly().GetName().Version.ToString());
            Logger = LogManager.GetCurrentClassLogger();
        }

        public static void Main(string[] args)
        {
            try
            {
                Logger.Info("Application started...");

                using (var container = new Container()
                    .RegisterUnitOfWorkDependencies(
                        UoWImplementationOption.EntityFramework6,
                        ConfigurationManager.ConnectionStrings["SimplePersistenceExample"].ConnectionString))
                {
                    container.Register<Program>();

                    container.GetInstance<Program>().Run();
                }
            }
            catch (Exception e)
            {
                Logger.Fatal(e, "Application terminated with an unexpected error");
            }
            finally
            {
                Logger.Info("Application terminated. Press <enter> to exit...");
                Console.ReadLine();
            }
        }

        #endregion

        public Program(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public void Run()
        {
            Logger.Info("Running tests");

            var logs = _factory.GetAndReleaseAfterExecuteAndCommit<IUnitOfWorkFactory, IExampleUnitOfWork, Log[]>(uow =>
            {
                return uow.Logging.Logs.Query().OrderByDescending(e => e.Id).Take(50).ToArray();
            });
        }
    }
}
