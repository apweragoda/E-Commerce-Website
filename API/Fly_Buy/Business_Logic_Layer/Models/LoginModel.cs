using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
