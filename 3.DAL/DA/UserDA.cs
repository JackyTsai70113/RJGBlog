using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DAL.Data;

namespace DAL.DA.Interfaces
{
    public class UserDA : IUserDA
    {
        public List<User> GetAll()
        {
            var user = new User
            {
                Id = 1,
                Name = "Jacky",
                Email = "j10926jacky@gmail.com",
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow
            };
            using (var db = new RJGDbContext())
            {
                Console.WriteLine("Inserting a new User");
                db.Add(user);
                db.SaveChanges();
            }
            List<User> users;
            using (var db = new RJGDbContext())
            {
                Console.WriteLine("Read Users");
                users = db.Users.ToList();
            }
            return users;
        }
    }
}