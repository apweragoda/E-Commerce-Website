using Data_Access_Layer.Entities;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Business_Logic_Layer.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
