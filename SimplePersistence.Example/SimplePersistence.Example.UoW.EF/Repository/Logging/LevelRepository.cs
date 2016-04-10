using System.Data.Entity;
using System.Linq;
using SimplePersistence.Example.Models.Logging;
using SimplePersistence.Example.UoW.Repository.Logging;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.UoW.EF.Repository.Logging
{
    public class LevelRepository : EFQueryableRepository<Level, string>, ILevelRepository
    {
        public LevelRepository(DbContext context) : base(context)
        {

        }

        public override IQueryable<Level> QueryById(string id)
        {
            return Query().Where(e => e.Id == id);
        }
    }
}