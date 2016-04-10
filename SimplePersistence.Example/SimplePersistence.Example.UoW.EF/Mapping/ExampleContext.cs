using System.Data.Entity;
using SimplePersistence.Model.EF.Fluent;

namespace SimplePersistence.Example.UoW.EF.Mapping
{
    public class ExampleContext : DbContext
    {
        public ExampleContext() : this("name=Example")
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
                    .Property(e => e.Id).HasMaxLength(128);
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
