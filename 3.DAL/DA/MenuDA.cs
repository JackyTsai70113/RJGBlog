using Core.Data;
using Core.Data.Entities;
using DAL.DA.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;


namespace DAL.DA
{
    public class MenuDA : BaseDA<Menu>, IMenuDA
    {
        private readonly ILogger<MenuDA> _logger;

        public MenuDA(ILogger<MenuDA> logger, RJGDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Menu> GetList()
        {
            List<Menu> blogs = _context.Menu.ToList();
            return blogs;
        }
    }
}
