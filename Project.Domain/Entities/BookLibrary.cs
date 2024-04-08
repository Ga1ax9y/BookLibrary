using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class BookLibrary 
    {
        public string UserId { get; set; }
        public List<Book> Books { get; set; }
        public Book CurrentBook { get; set; } 
        public BookLibrary()
        {
            Books = new List<Book>();
        }

    }
}
