using System.Collections.Generic;
using System.Linq;

namespace DAL.DA.Base
{
    public interface IBaseDA<T>
    {
        int SaveChanges();

        IEnumerable<T> GetPagedEnumerable(IQueryable<T> query, int skip, int limit, out int lastPageIndex);
    }
}