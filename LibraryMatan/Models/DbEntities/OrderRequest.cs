using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryMatan.Models
{
    public class OrderRequest
    {
        public int Id { get; set; }
        [Required]
        public int OrderRequestTypeId { get; set; }

        [Required] 
        public int BookId { get; set; }
        public int MembershipId { get; set; }
        public string FreeBookText { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int StatusId { get; set; }
    }
}