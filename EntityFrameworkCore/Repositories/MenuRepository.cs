using Domain;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore.Repositories
{
    public class MenuRepository:RepositoriesBase<Menu>,IMenuRepository
    {
        public MenuRepository(WeaverDbContext dbContext) : base(dbContext)
        {

        }
    }
}
