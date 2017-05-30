using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameworkCore
{
    public  static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =  new WeaverDbContext(serviceProvider.GetRequiredService<DbContextOptions<WeaverDbContext>>()))
            {
                if (context.tb_User.Any())
                {
                    return;
                }

                Guid departmentId = Guid.NewGuid();

                context.tb_Department.Add(new Department
                {
                    Id = departmentId,
                    Name = "Defense of the Ancients",
                    ParentId = Guid.Empty
                });

                context.tb_User.Add(new User
                {
                    UserName = "Admin",
                    Password = "12345",
                    Name = "Roshan",
                    DepartmentId = departmentId
                });

                context.tb_Menu.AddRange(new Menu
                {
                    Name = "Organizational Management",
                    Code = "Department",
                    SerialNumber = 0,
                    ParentId = Guid.Empty,
                    Icon = "fa fa-link"
                },
                new Menu {
                    Name = "Role Management",
                    Code = "Role",
                    SerialNumber = 1,
                    ParentId = Guid.Empty,
                    Icon = "fa fa-link"
                },
                new Menu {
                    Name = "User Management",
                    Code = "User",
                    SerialNumber = 2,
                    ParentId = Guid.Empty,
                    Icon = "fa fa-link"
                }, 
                new Menu {
                    Name = "Features Management",
                    Code = "Department",
                    SerialNumber = 3,
                    ParentId = Guid.Empty,
                    Icon = "fa fa-link"
                });

                context.SaveChanges();
            }
        }
    }
}
