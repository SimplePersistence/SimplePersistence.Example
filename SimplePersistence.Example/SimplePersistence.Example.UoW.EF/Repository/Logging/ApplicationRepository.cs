using System.Data.Entity;
using System.Linq;
using SimplePersistence.Example.Models.Logging;
using SimplePersistence.Example.UoW.Repository.Logging;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.UoW.EF.Repository.Logging
{
    public class ApplicationRepository : EFQueryableRepository<Application, string>, IApplicationRepository
    {
        public ApplicationRepository(DbContext context) : base(context)
        {

        }

        public override IQueryable<Application> QueryById(string id)
        {
            return Query().Where(e => e.Id == id);
        }
    }
}
