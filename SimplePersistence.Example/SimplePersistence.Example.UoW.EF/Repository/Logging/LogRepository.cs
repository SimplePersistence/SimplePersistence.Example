using System.Data.Entity;
using System.Linq;
using SimplePersistence.Example.Models.Logging;
using SimplePersistence.Example.UoW.Repository.Logging;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.UoW.EF.Repository.Logging
{
    public class LogRepository : EFQueryableRepository<Log, long>, ILogRepository
    {
        public LogRepository(DbContext context) : base(context)
        {

        }

        public override IQueryable<Log> QueryById(long id)
        {
            return Query().Where(e => e.Id == id);
        }
    }
}