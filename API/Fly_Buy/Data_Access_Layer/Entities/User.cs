using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data_Access_Layer.Repository.Entities
{
    public class User 
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
