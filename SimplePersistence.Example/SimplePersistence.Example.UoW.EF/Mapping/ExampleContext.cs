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

using System.Data.Entity;
using SimplePersistence.Model.EF.Fluent;

namespace SimplePersistence.Example.UoW.EF.Mapping
{
    public class ExampleContext : DbContext
    {
        public ExampleContext() : this("name=SimplePersistenceExample")
        {

        }

        public ExampleContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Logging

            modelBuilder.Entity<Models.Logging.Application>(cfg =>
            {
                cfg.HasKey(e => e.Id)
                    .Property(e => e.Id).HasMaxLength(128);
                cfg.Property(e => e.Description).IsRequired().HasMaxLength(512);

                cfg.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta().MapByteArrayVersion();
            }, "Applications", "Logging");

            modelBuilder.Entity<Models.Logging.Level>(cfg =>
            {
                cfg.HasKey(e => e.Id)
                    .Property(e => e.Id).HasMaxLength(8);
                cfg.Property(e => e.Description).IsRequired().HasMaxLength(512);

                cfg.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta().MapByteArrayVersion();
            }, "Levels", "Logging");

            modelBuilder.Entity<Models.Logging.Log>(cfg =>
            {
                cfg.HasIdentityKeyAsLong();
                cfg.HasRequired(e => e.Level).WithMany(e => e.Logs).Map(m => m.MapKey("Level"));
                cfg.Property(e => e.Logger).IsRequired().HasMaxLength(256);
                cfg.Property(e => e.Message).IsRequired();
                cfg.Property(e => e.Exception).IsOptional();
                cfg.HasRequired(e => e.Application).WithMany(e => e.Logs).Map(m => m.MapKey("Application"));
                cfg.Property(e => e.Thread).IsOptional().HasMaxLength(128);
                cfg.Property(e => e.MachineName).IsRequired().HasMaxLength(128).AddIndex();
                cfg.Property(e => e.AssemblyVersion).IsRequired().HasMaxLength(32).AddIndex();

                cfg.MapCreatedMeta(onNeedsIndex: true);
            }, "Logs", "Logging");

            #endregion
        }
    }
}
