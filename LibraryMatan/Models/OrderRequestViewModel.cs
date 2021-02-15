using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryMatan.Models
{
    public class OrderRequestViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Author { get; set; }
        public int? GenreId { get; set; }
        public int ActionToDo { get; set; }

    }
}
