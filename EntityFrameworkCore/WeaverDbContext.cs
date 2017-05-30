using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore
{
    public class WeaverDbContext:DbContext
    {
        public WeaverDbContext(DbContextOptions<WeaverDbContext> options) : base(options)
        {

        }

        public DbSet<Department> tb_Department { get; set; }
        public DbSet<Menu> tb_Menu { get; set; }
        public DbSet<Role> tb_Role { get; set; }
        public DbSet<User> tb_User { get; set; }
        public DbSet<UserRole> tb_UserRole { get; set; }
        public DbSet<RoleMenu> tb_RoleMenu { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<RoleMenu>().HasKey(rm => new { rm.RoleId, rm.MenuId });

            base.OnModelCreating(builder);
        }
    }
}
