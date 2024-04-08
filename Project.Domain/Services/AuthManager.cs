using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Services
{
    public class AuthManager
    {
        public List<User> Users { get; set; }
        public User CurrentUser = null;
        public AuthManager()
        {
            Users = new List<User>();
            Users.Add(new User("Admin", "pass"));
        }
        public void Registration(string login, string password)
        {
            User new_user = new User(login, password);
            Users.Add(new_user);
            CurrentUser = new_user;

        }
        public void Login(string login, string password)
        {
            CurrentUser = null;
            foreach (var user in Users)
            {
                if (user.Login == login && user.Password == password)
                {
                    CurrentUser = user;
                    break;
                }

            }
             

        }

    }
}
