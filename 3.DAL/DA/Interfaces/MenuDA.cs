using Core.Data;
using Core.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.DA.Interfaces
{
    public class MenuDA : BaseDA, IMenuDA
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
