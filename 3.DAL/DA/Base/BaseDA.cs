
using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Models.DTO.Pagination;

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

        public IEnumerable<T> GetPagedEnumerable(IQueryable<T> query, int skip, int limit, out int total){
            total = query.Count();
            IEnumerable<T> enumerable = query.Skip(skip)
                                             .Take(limit)
                                             .AsEnumerable<T>();
            return enumerable;
        }

        private IEnumerable<T> ToPagedList(IQueryable<T> query, PaginationModel pagination, out int total){
            total = query.Count();
            IEnumerable<T> enumerable = query.Skip(pagination.skipCount)
                                             .Take(pagination.limitCount)
                                             .AsEnumerable<T>();
            return enumerable;
        }
    }
}