using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weaver.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="User Name can not Empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password can not Empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
