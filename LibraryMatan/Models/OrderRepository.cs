using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMatan.Models
{
    public class OrderRepository : InMemoryRepository<OrderRequest>
    {
        internal void InsertOrder(Book book, int actionToDo, int memberId)
        {
            throw new NotImplementedException();
        }
    }
}
