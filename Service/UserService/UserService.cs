using Domain;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.UserService
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userReporitory;

        public UserService(IUserRepository userRepository)
        {
              _userReporitory = userRepository;
        }

        public User CheckUser(string username,string password)
        {
            return _userReporitory.CheckUser(username, password);
        }
    }
}
