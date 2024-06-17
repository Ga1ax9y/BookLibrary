
using MyProjectLibrary;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Drawing;
using System.Net;

namespace MyProjectLibrary
{
    public static class IdGenerator
    {
        private static int counter = 1;
        public static int counterB = 1;
        public static int GenerateUserUniqueId()
        {
            int newId = counter;
            counter++;
            return newId;
        }
        public static int GenerateBookUniqueId()
        {
            int newId = counterB;
            counterB++;
            return newId;
        }
    }
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Path { get; set; }
        public List<int> Bookmarks { get; set; }
        public int AmountOfPages { get; set; }
        public int CurrentPage { get; set; }
        public int UserId { get; set; }
        public string Bookinfo => $"Название: {Title}    Автор: {Author} -    Дата: {Date}";
        public Book()
        {

        }
        public Book(string title, string path,int userId, string date, string author = "Неизвестно")
        {
            Id = IdGenerator.GenerateBookUniqueId();
            Title = title;
            Author = author;
            Path = path;
            UserId = userId;
            Date = date;
            Bookmarks = new List<int>();
        }



    }
    public class BookLibrary
    {
        public int UserId { get; set; }
        public List<Book> Books { get; set; }
        public Book CurrentBook { get; set; }
        public BookLibrary()
        {
            Books = new List<Book>();
        }

    }
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public BookLibrary Library { get; set; } = new BookLibrary();
        public User()
        {

        }

        public User(string login, string password)
        {
            this.Login = login;
            this.Password = password;
            Library = new BookLibrary();
            Id = IdGenerator.GenerateUserUniqueId();
            Library.UserId = Id;
        }

    }
    public class AuthService
    {
        public List<User> Users { get; set; }
        public User CurrentUser = null;

        public AuthService()
        {
            Users = new List<User>();

        }
        public async Task<bool> isLoggedAsync()
        {
            await Task.Delay(2000);
            return false;
        }
        public void Registration(string login, string password)
        {
            User new_user = new User(login, password);
            Database.SaveUser(new_user);
            Users.Add(new_user);
            CurrentUser = new_user;

        }
        public void Logout()
        {
            CurrentUser = null; 
        }
        public bool Login(string login, string password)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(Database.LoadConnectionString()))
                {
                    string query = "SELECT * FROM User WHERE Login = @Login AND Password = @Password";
                    var user = cnn.QuerySingleOrDefault<User>(query, new { Login = login, Password = password });

                    if (user != null)
                    {
                        CurrentUser = user;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during login: {ex.Message}");
            }

            return false;
        }
    }

    public class LibraryService
    {
        BookLibrary bookLibrary;

        public LibraryService(BookLibrary bookLibrary)
        {
            this.bookLibrary = bookLibrary;
        }
        public void AddBook(string title, string path,int userId, string date,string author = "Неизвестно")
        {
            var new_book = new Book(title, path, userId,date, author);
            bookLibrary.Books.Add(new_book);
            Database.SaveBook(new_book);

        }
        public void UpadateBook(int bookId,string newTitle, string newAuthor, string newPath)
        {
            Book edBook;
            edBook = Database.GetBookById(bookId);
            edBook.Title = newTitle;
            edBook.Author = newAuthor;
            edBook.Path = newPath;
            edBook.Date = (DateTime.Now).ToString();
            Database.UpdateBook(edBook);

        }
        public void RemoveBook(int bookId)
        {
            Book book = Database.GetBookById(bookId);
            bookLibrary.Books.Remove(book);

            Database.DeleteBook(bookId);
        }

        public Book FindBookById(int bookId)
        {
            return Database.GetBookById(bookId);

        }
        public List<Book> GetUserBooks(int userId)
        {
            return Database.GetUserBooks(userId);
        }
        public List<Book> GetAllBooks()
        {
            return bookLibrary.Books;
        }

        public void SortByDate()
        {
            bookLibrary.Books = bookLibrary.Books.OrderByDescending(x => x.Date).ToList();
        }
        public void SortByName()
        {
            bookLibrary.Books = bookLibrary.Books.OrderBy(x => x.Title).ToList();
            //Console.WriteLine(bookLibrary.Books);
        }
        public void SortByAuthor()
        {
            bookLibrary.Books = bookLibrary.Books.OrderBy(x => x.Author).ToList();
        }
    }
    public class UserService
    {
        private User CurrentUser = null;

        public UserService(AuthService manager)
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
