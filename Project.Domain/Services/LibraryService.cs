using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Services
{
    public class LibraryService
    {
        private BookLibrary Library { get; set; }

        public LibraryService(BookLibrary library) 
        {
            Library = library;
        }
        public void AddBook(string title, string path, string author = "Неизвестно")
        {
            var new_book = new Book(title, path, author);
            if (Library.Books.Find(t => t.Title == title) == null)
            {
                Library.Books.Add(new_book);
                new_book.UserId = Library.UserId;
                Library.CurrentBook = new_book;
            }

        }
        public void RemoveBook(Book book)
        {
            if (book == Library.CurrentBook) 
            {
                Library.CurrentBook = null;
            }
            Library.Books.Remove(book);
        }

        public Book FindBookByTitle(string title)
        {
            var book = Library.Books.Find(x => x.Title == title);
            return book;

        }
        public List<Book> GetAllBooks()
        {
            return Library.Books;
        }

        public void SortByDate()
        {
            Library.Books = Library.Books.OrderByDescending(x => x.Date).ToList();
        }
        public void SortByName()
        {
            Library.Books = Library.Books.OrderBy(x => x.Title).ToList();

        }
    }
}
