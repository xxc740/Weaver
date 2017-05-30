using Domain;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameworkCore.Repositories
{
    public class UserRepository:RepositoriesBase<User>, IUserRepository
    {
        public UserRepository(WeaverDbContext dbContext) : base(dbContext) { }

        public User CheckUser(string userName,string Password)
        {
            return _dbContext.Set<User>().FirstOrDefault(i => i.UserName == userName && i.Password == Password);
        }
    }
}
