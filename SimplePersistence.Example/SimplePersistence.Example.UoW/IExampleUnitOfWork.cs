using SimplePersistence.Example.UoW.WorkArea;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.UoW
{
    public interface IExampleUnitOfWork : IUnitOfWork
    {
        #region Work Areas

        ILoggingWorkArea Logging { get; }

        #endregion
    }
}
