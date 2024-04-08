using Project.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public BookLibrary Library { get; set; }

        public User(string login, string password)
        {
            this.Login = login;
            this.Password = password;
            Library = new BookLibrary();
            Id = IdGenerator.GenerateUniqueId();
            Library.UserId = Id;
        }

    }
}
