using System;
using System.Collections.Generic;
using System.Text;

namespace Service.MenuService
{
    public class MenuServiceModel
    {
        public Guid Id { get; set; }

        public Guid ParentId { get; set; }

        public int SerialNumber { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Url { get; set; }

        public int Type { get; set; }

        public string Icon { get; set; }

        public string Remarks { get; set; }
    }
}
