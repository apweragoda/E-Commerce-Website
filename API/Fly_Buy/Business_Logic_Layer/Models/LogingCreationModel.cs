using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class LogingCreationModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool RememberMe { get; set; }
    }
}
