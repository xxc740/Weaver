﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User:Entity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Remarks { get; set; }

        public Guid CreateUserId { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime LastLoginTime { get; set; }

        public int LoginTimes { get; set; }

        public int IsDeleted { get; set; }

        public Guid DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
