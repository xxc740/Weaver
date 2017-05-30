using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Department:Entity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Manager { get; set; }

        public string ContactNumber { get; set; }

        public string Remarks { get; set; }

        public Guid ParentId { get; set; }

        public Guid CreateUserId { get; set; }

        public DateTime? CreateTime { get; set; }

        public int IsDelete { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
