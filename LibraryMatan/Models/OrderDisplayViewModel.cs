using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMatan.Models
{
    public class OrderDisplayViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MembershipId { get; set; }
        public string ActionToDo { get; set; }
        public string StatusId { get; set; }
    }
}
