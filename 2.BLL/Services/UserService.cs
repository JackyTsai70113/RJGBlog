using System.Collections.Generic;
using BLL.Services.Interfaces;
using Core;
using DAL.DA.Interfaces;

namespace BLL.Services {

    public class UserService : IUserService {
        private readonly IUserDA _userDA;

        public UserService(IUserDA userDA) {
            _userDA = userDA;
        }

        public List<User> GetUsers() {
            var users = _userDA.GetAll();
            //string value = "abcdefg";
            //db.StringSet("mykey", value);

            //string value = db.StringGet("mykey");
            //Console.WriteLine(value); // writes: "abcdefg"
            return users;
        }
    }
}