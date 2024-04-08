using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Services
{
    public class UserService
    {
        private User CurrentUser = null;

        public UserService(AuthManager manager) 
        {
            CurrentUser = manager.CurrentUser; 
        }
        public Book GetUserCurrentBook()
        {
            return CurrentUser.Library.CurrentBook;
        }
        public void SetCurrentBook(Book book) 
        {
            CurrentUser.Library.CurrentBook = book;
        }
        public User GetCurrentUser() 
        {
            return CurrentUser;
        }
        public BookLibrary GetUserLibrary() 
        {
            return CurrentUser.Library;
        }
        public List<Book> GetUserBookList() 
        {
            return CurrentUser.Library.Books;
        }
    }
}
