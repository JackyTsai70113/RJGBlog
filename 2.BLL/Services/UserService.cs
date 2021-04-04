using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BLL.Services.Interfaces;
using Core;
using Core.Models.DTO.Views;
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

        public bool CreateUser(RegisterViewModel model)
        {
            string password = ComputeHash(model.Password);
            DateTime utcNow = DateTime.UtcNow;
            User user = new User
            {
                Account = model.Email,
                Password = password,
                Name = "新使用者",
                Email = model.Email,
                CreateTime = utcNow,
                UpdateTime = utcNow
            };
            _userDA.Create(user);
            return true;
        }

        private string ComputeHash(string input)
        {
            HashAlgorithm sha256Hash = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashedBytes = sha256Hash.ComputeHash(inputBytes);

            string resultWithDash = BitConverter.ToString(hashedBytes);

            string result = resultWithDash.Replace("-", "");

            return result;
        }
    }
}