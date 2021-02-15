using System;
using System.Collections.Generic;

namespace LibraryMatan.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? VerifiedNameId { get; set; }
        public int? VerifiedAuthorId { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int RequestedTimes { get; set; }
        public int? GenreId { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
