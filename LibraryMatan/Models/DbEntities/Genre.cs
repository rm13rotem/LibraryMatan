using System;
using System.Collections.Generic;

namespace LibraryMatan.Models
{
    public partial class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
