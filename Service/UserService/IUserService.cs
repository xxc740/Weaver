using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IUserService
    {
        User CheckUser(string userName, string Password);
    }
}
