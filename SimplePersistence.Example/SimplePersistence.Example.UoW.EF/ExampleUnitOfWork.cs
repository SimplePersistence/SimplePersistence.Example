using SimplePersistence.Example.UoW.EF.Mapping;
using SimplePersistence.Example.UoW.EF.WorkArea;
using SimplePersistence.Example.UoW.WorkArea;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.UoW.EF
{
    public class ExampleUnitOfWork : EFUnitOfWork<ExampleContext>, IExampleUnitOfWork
    {
        public ExampleUnitOfWork(ExampleContext context) : base(context)
        {
            Logging = new LoggingWorkArea(context);
        }

        public ILoggingWorkArea Logging { get; }
    }
}
