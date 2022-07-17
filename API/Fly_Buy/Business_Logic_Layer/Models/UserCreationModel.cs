using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class UserCreationModel 
    {

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password should minimum 8 charactors and should be contain atleast one block letter and number")]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
