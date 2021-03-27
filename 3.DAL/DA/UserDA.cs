using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.DA.Interfaces
{
    public class UserDA : IUserDA
    {
        private readonly RJGDbContext _context;
        public UserDA(RJGDbContext context)
        {
            _context = context;
        }
        public List<User> GetAll()
        {
            List<RoleUser> roleUsers = _context.RoleUser.Include(x=>x.Role).ToList();
            List<User> users = _context.User.Include(x => x.RoleUser).ToList();
            return users;
        }
    }
}