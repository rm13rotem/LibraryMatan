using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMatan.Models
{
    public class OrderRepository : InMemoryRepository<OrderRequest>
    {
        public void InsertOrder(Book book, int actionToDo, int memberId)
        {
            OrderRequest newOrderRequest = new OrderRequest()
            {
                FreeBookText = book.Name,
                OrderRequestTypeId = actionToDo, StatusId = 1, MembershipId = memberId
            };
            InMemoryRepository<OrderRequest> repository = new InMemoryRepository<OrderRequest>();
            var requestExists = repository.GetAll().FirstOrDefault(x => x.FreeBookText.Contains(book.Name));
            if (requestExists == null || requestExists.OrderRequestTypeId != actionToDo)
            {
                repository.TryInsert(newOrderRequest);
                return;
            }
            // else;
            return; // already exists;
        }
    }
}
