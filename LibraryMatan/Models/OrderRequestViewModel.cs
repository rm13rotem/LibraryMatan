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
        public int StatusId { get; set; }

        public OrderRequest ToOrderRequest()
        {
            return new OrderRequest()
            {
                FreeBookText = Name,
                OrderRequestTypeId = ActionToDo,
                StatusId = 1,
                MembershipId = MembershipId
            };
        }

        public int MembershipId { get; internal set; }

        public Book ToBook()
        {
            return new Book()
            {
                GenreId = GenreId,
                Author = Author,
                Name = Name
            };
        }
    }
}
