using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IRepositories
{
    public interface IUserRepository:IRepository<User>
    {
        User CheckUser(string UserName, string Password);
    }
}
