#region License
// // The MIT License (MIT)
// // 
// // Copyright (c) 2016 SimplePersistence
// // 
// // Permission is hereby granted, free of charge, to any person obtaining a copy
// // of this software and associated documentation files (the "Software"), to deal
// // in the Software without restriction, including without limitation the rights
// // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// // copies of the Software, and to permit persons to whom the Software is
// // furnished to do so, subject to the following conditions:
// // 
// // The above copyright notice and this permission notice shall be included in all
// // copies or substantial portions of the Software.
// // 
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// // SOFTWARE.
#endregion

using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public IQueryable<Log> QueryFilterByLevelWithPagination(string level, int skip = 0, int take = 50)
        {
            return Query().Where(e => e.Level.Id == level).Skip(skip).Take(take);
        }

        public Log[] FilterByLevelWithPagination(string level, int skip = 0, int take = 50)
        {
            return QueryFilterByLevelWithPagination(level, skip, take).ToArray();
        }

        public async Task<Log[]> FilterByLevelWithPaginationAsync(
            string level, int skip = 0, int take = 50, CancellationToken ct = new CancellationToken())
        {
            return await QueryFilterByLevelWithPagination(level, skip, take).ToArrayAsync(ct);
        }
    }
}