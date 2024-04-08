using Project.Domain.Helper;

namespace Project.Domain.Entities
{
    public class Book
    {
        public string Id { get; set; }
        public string Title{ get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Path { get; set; }
        public List<int> Bookmarks { get; set; }
        public int AmountOfPages { get; set; }
        public int CurrentPage { get; set; }
        public List<(int, int)> Markers { get; set; }
        public string UserId { get; set; }
        public Book(string title, string path, string author = "Неизвестно")
        {
            Id = IdGenerator.GenerateUniqueId();
            Title = title;
            Author = author;
            Path = path;
            Date = DateTime.Now;
            Bookmarks = new List<int>();
            Markers = new List<(int, int)>();
        }
        public void AddBookMark(int page)
        {
            Bookmarks.Add(page);
        }
        public void DeleteBookMark(int page)
        {
            Bookmarks.Remove(page);
        }
        public void AddHighlight(int x_coord, int y_coord)
        {
            Markers.Add((x_coord, y_coord));
        }
        public void EraseHighlight(int x_coord, int y_coord)
        {
            Markers.Remove((x_coord, y_coord));
        }


    }
}
