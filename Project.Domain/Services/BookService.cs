using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Services
{
    public class BookService
    {
        private Book CurrentBook;
        public BookService(Book book) 
        { 
            CurrentBook = book;
        }
        public void AddBookMark(int page)
        {
            CurrentBook.Bookmarks.Add(page);
        }
        public void DeleteBookMark(int page)
        {
            CurrentBook.Bookmarks.Remove(page);
        }
        public void AddHighlight(int x_coord, int y_coord)
        {
            CurrentBook.Markers.Add((x_coord, y_coord));
        }
        public void EraseHighlight(int x_coord, int y_coord)
        {
            CurrentBook.Markers.Remove((x_coord, y_coord));
        }
        public List<int> GetBookmarks()
        {
            return CurrentBook.Bookmarks;
        }

    }
}
