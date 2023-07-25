
using System.Collections.Generic;
using System.Linq;
using Core.Data;

namespace DAL.DA.Interfaces
{
    public class BaseDA<T>
    {
        protected RJGDbContext _context;
        public int SaveChanges()
        {
            int changeCount = _context.SaveChanges();
            return changeCount;
        }

        public IEnumerable<T> GetPagedEnumerable(IQueryable<T> query, int skip, int limit, out int lastPageIndex)
        {
            lastPageIndex = query.Count() / limit + 1;
            IEnumerable<T> enumerable = query.Skip(skip)
                                             .Take(limit)
                                             .AsEnumerable<T>();
            return enumerable;
        }
    }
}