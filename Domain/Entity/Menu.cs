using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
     public class Menu:Entity
    {
        public Guid ParentId { get; set; }

        public int SerialNumber { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Url { get; set; }

        //类型：0导航菜单；1操作菜单
        public int Type { get; set; }

        public string Icon { get; set; }

        public string Remarks { get; set; }
    }
}
