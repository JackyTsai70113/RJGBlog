using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.DA.Interfaces
{
    public class UserDA : BaseDA, IUserDA
    {
        private readonly RJGDbContext _context;
        private readonly ILogger<UserDA> _logger;
        public UserDA(ILogger<UserDA> logger, RJGDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        public List<User> GetAll()
        {
            List<RoleUser> roleUsers = _context.RoleUser.Include(x => x.Role).ToList();
            List<User> users = _context.User.Include(x => x.RoleUser).ToList();
            return users;
        }

        public void Create(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }
    }
}