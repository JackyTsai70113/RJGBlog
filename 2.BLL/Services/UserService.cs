using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BLL.Services.Interfaces;
using Core;
using Core.Models.DTO.Account;
using DAL.DA.Interfaces;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDA _userDA;

        public UserService(IUserDA userDA)
        {
            _userDA = userDA;
        }

        public List<User> GetUsers()
        {
            var users = _userDA.GetAll();
            return users;
        }
    }
}