using System;
using System.Security.Cryptography;
using System.Text;
using BLL.Services.Interfaces;
using Core;
using Core.Models.DTO.Account;
using DAL.DA.Interfaces;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserDA _userDA;

        public AccountService(IUserDA userDA)
        {
            _userDA = userDA;
        }

        public bool Register(RegisterAccountModel model)
        {
            string password = ComputeHash(model.Password);
            DateTime utcNow = DateTime.UtcNow;
            User user = new User
            {
                Account = model.Account,
                Password = password,
                Name = "新使用者",
                Email = model.Email,
                CreateTime = utcNow,
                UpdateTime = utcNow
            };
            _userDA.Create(user);
            return true;
        }

        public bool Login(LoginAccountModel model)
        {
            // 取db 的使用者
            User dbUser;
            if (model.Account.Contains("@"))
            {
                dbUser = _userDA.GetByEmail(model.Account);
            }
            else
            {
                dbUser = _userDA.GetByAccount(model.Account);
            }

            // 若使用者為空則表示沒有帳號
            if (dbUser == null)
            {
                return false;
            }

            // 驗證密碼
            string password = ComputeHash(model.Password);
            if (dbUser.Password == password)
            {
                return true;
            }
            return false;
        }

        private string ComputeHash(string input)
        {
            HashAlgorithm sha256 = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashedBytes = sha256.ComputeHash(inputBytes);

            string resultWithDash = BitConverter.ToString(hashedBytes);

            string result = resultWithDash.Replace("-", "");

            return result;
        }
    }
}