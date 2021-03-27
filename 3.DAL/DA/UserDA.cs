using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DAL.Data;

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
            List<User> users = _context.User.ToList();
            return users;
        }
    }
}