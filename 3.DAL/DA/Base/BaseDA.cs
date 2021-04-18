
using Core.Data;

namespace DAL.DA.Interfaces
{
    public class BaseDA
    {
        protected RJGDbContext _context;
        public int SaveChanges()
        {
            int changeCount = _context.SaveChanges();
            return changeCount;
        }
    }
}