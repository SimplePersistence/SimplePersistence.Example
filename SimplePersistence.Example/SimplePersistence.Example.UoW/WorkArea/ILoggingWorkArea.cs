using SimplePersistence.Example.UoW.Repository.Logging;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.UoW.WorkArea
{
    public interface ILoggingWorkArea : IWorkArea
    {
        IApplicationRepository Applications { get; }

        ILevelRepository Levels { get; }

        ILogRepository Logs { get; }
    }
}
