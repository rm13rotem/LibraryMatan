using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryMatan.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? VerifiedNameId { get; set; }
        public int? VerifiedAuthorId { get; set; }
        [Required]
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int RequestedTimes { get; set; }
        public int? GenreId { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public  bool IsValid()
        {

            if (RequestedTimes == 0)
                RequestedTimes = 1;
            if (CreatedDateTime < DateTime.Now.AddMonths(-1))
                CreatedDateTime = DateTime.UtcNow;
            if (string.IsNullOrEmpty(Name)) return false;
            if (string.IsNullOrEmpty(Author)) return false;
            return true;
        }
    }
}
