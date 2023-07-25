using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Data.Entities;
using DAL.DA.Interfaces;

namespace DAL.DA
{
    public class MenuDA : BaseDA<Menu>, IMenuDA
    {
        public MenuDA(RJGDbContext context)
        {
            _context = context;
        }

        public List<Menu> GetList()
        {
            List<Menu> blogs = _context.Menu.ToList();
            return blogs;
        }
    }
}
