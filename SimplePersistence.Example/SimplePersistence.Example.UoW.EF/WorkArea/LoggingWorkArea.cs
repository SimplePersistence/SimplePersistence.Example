using SimplePersistence.Example.UoW.EF.Mapping;
using SimplePersistence.Example.UoW.EF.Repository.Logging;
using SimplePersistence.Example.UoW.Repository.Logging;
using SimplePersistence.Example.UoW.WorkArea;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.UoW.EF.WorkArea
{
    public class LoggingWorkArea : EFWorkArea<ExampleContext>, ILoggingWorkArea
    {
        public LoggingWorkArea(ExampleContext context) : base(context)
        {
            Applications = new ApplicationRepository(context);
            Levels = new LevelRepository(context);
            Logs = new LogRepository(context);
        }

        public IApplicationRepository Applications { get; }

        public ILevelRepository Levels { get; }

        public ILogRepository Logs { get; }
    }
}
