using System;
using System.Collections.Generic;

namespace LibraryMatan.Models
{
    public partial class VerifiedAuthor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
