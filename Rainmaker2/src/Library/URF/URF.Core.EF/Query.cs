using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace URF.Core.EF
{
    public static class QuerableExtensions
    {
        public static async Task<(IEnumerable<TEntity> collection, int totalRecords)> SelectPageAsync<TEntity>(this IQueryable<TEntity> query, int page, int pageSize)
        {
            int totalCount = await query.CountAsync();
            query = query.Skip((page - 1) * pageSize);
            query = query.Take(pageSize);
            return (await query.ToListAsync(), totalCount);
        }
    }
}
