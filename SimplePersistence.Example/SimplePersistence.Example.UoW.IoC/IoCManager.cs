﻿#region License
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
using SimpleInjector;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.UoW.IoC
{
    public static class IoCManager
    {
        public static Container RegisterUnitOfWorkDependencies(
            this Container container, UoWImplementationOption versionOption, string connectionString)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));

            switch (versionOption)
            {
                case UoWImplementationOption.EntityFramework6:
                    container.Register(() => new EF.Mapping.ExampleContext(), Lifestyle.Transient);
                    container.Register<IExampleUnitOfWork, EF.ExampleUnitOfWork>(Lifestyle.Transient);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(versionOption), versionOption, null);
            }
            container.Register<IUnitOfWorkFactory>(() => new UnitOfWorkFactory(container), Lifestyle.Singleton);

            return container;
        }
    }
}
