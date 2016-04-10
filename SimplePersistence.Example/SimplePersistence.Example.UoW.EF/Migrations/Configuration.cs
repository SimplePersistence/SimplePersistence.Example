using System.Linq;
using SimplePersistence.Example.Models.Logging;
using SimplePersistence.Example.UoW.EF.Mapping;
using SimplePersistence.Model.Helper;

namespace SimplePersistence.Example.UoW.EF.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ExampleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ExampleContext context)
        {
            const string username = "script.migration";

            #region Levels

            var levels = new[]
            {
                new Level
                {
                    Id = Level.Trace,
                    Description = "Trace"
                }.InitializedBy(username),
                new Level
                {
                    Id = Level.Debug,
                    Description = "Debug"
                }.InitializedBy(username),
                new Level
                {
                    Id = Level.Info,
                    Description = "Information"
                }.InitializedBy(username),
                new Level
                {
                    Id = Level.Error,
                    Description = "Error"
                }.InitializedBy(username),
                new Level
                {
                    Id = Level.Fatal,
                    Description = "Fatal"
                }.InitializedBy(username)
            };

            var levelsSet = context.Set<Level>();
            foreach (var level in levels.Where(level => !levelsSet.Any(e => e.Id == level.Id)))
            {
                levelsSet.Add(level);
            }

            #endregion

            #region Applications

            var applications = new[]
            {
                new Application
                {
                    Id = Application.CommandLine,
                    Description = "Command Line application with example operations"
                }.InitializedBy(username)
            };

            var applicationsSet = context.Set<Application>();
            foreach (
                var application in applications.Where(application => !applicationsSet.Any(e => e.Id == application.Id)))
            {
                applicationsSet.Add(application);
            }

            #endregion
        }
    }
}
