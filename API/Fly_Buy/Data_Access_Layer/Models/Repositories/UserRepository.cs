using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Access_Layer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FlyBuyDbContext ctx;

        public UserRepository(FlyBuyDbContext ctx)
        {
            this.ctx = ctx;
        }
        public ICollection<User> GetAllUsers()
        {
            return ctx.Users.ToList();
        }
        public User GetUser(Guid id)
        {
            return ctx.Users
                .FirstOrDefault(o => o.Id == id);
        }

        public User AddUser(User user)
        {
            if (user == null)
                return null;

            ctx.Users.Add(user);
            ctx.SaveChanges();
            return user;
           
        }


        public User UpdateUser(User user)
        {
            if (user == null)
                return null;

            ctx.Users.Update(user);
            ctx.SaveChanges();
            return user;

        }

        public int DeleteUser(Guid id)
        {
            var user = ctx.Users
                        .FirstOrDefault(u => u.Id == id);
            if(user != null)
            {
                ctx.Users.Remove(user);
                Console.WriteLine("User deleted - ", user.FirstName);
                return ctx.SaveChanges();
            }
            return 0 ;
        }


        public User LoginUserWithEmail(string email, string password)
        {
            var user = ctx.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                Console.WriteLine("User not exist");
                return null;
            }
            if(user.Password != password)
            {
                Console.WriteLine("Password does not match");
                return null;
            }
            return user;
        }

    }
}
