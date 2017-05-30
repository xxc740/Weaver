using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public abstract class Entity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }

    public abstract class Entity : Entity<Guid> { }
}
