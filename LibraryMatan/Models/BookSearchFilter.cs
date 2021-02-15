using System.Collections.Generic;

namespace LibraryMatan.Models
{
    public class BookSearchFilter
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public int GenreId { get; set; }

        public List<Book> Result { get; set; }
    }
}
