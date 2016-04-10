using SimplePersistence.Example.Models.Logging;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.UoW.Repository.Logging
{
    public interface IApplicationRepository : IQueryableRepository<Application, string>
    {

    }
}
