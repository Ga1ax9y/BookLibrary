using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProjectLibrary
{
    public class Database
    {

        public static List<User> LoadUsers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>("select * from User",new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveUser(User user)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            { 
                cnn.Execute("insert into User (Login, Password) values (@Login,@Password)",user);
            }
            LoadConnectionString();
        }
        public static string LoadConnectionString(string id="Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        public static void SaveBook(Book book)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Book (Title, Author, Path, Date, userId) values (@Title, @Author, @Path, @Date, @UserId)", book);
            }
            LoadConnectionString();
        }
        public static List<Book> GetUserBooks(int userId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string query = "select * from Book where UserId = @UserId";
                return cnn.Query<Book>(query, new { UserId = userId }).AsList();
            }
        }
        public static Book GetBookById(int bookId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string query = "SELECT * FROM Book WHERE Id = @BookId";
                return cnn.QueryFirstOrDefault<Book>(query, new { BookId = bookId });
            }
        }
        public static void DeleteBook(int bookId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string query = "DELETE FROM Book WHERE Id = @BookId";
                cnn.Execute(query, new { BookId = bookId });
            }
        }
        public static void UpdateBook(Book book)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string query = "UPDATE Book " +
                               "SET Title = @Title, " +
                                   "Author = @Author, " +
                                   "Path = @Path, " +
                                   "[Date] = @Date " +
                               "WHERE Id = @Id";

                cnn.Execute(query, new
                {
                    book.Id,
                    book.Title,
                    book.Author,
                    book.Path,
                    book.Date
                });
            }
        }
    }

}
