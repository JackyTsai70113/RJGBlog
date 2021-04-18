using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.DA.Interfaces
{
    public class UserDA : BaseDA, IUserDA
    {
        private readonly ILogger<UserDA> _logger;
        public UserDA(ILogger<UserDA> logger, RJGDbContext context)
        {
            _logger = logger;
            _context = context;
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

        public User GetByAccount(string account)
        {
            User user = _context.User.Where(u => u.Account == account).FirstOrDefault();
            return user;
        }

        public User GetByEmail(string email)
        {
            User user = _context.User.Where(u => u.Email == email).FirstOrDefault();
            return user;
        }

    }
}