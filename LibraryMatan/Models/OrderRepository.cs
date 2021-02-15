using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMatan.Models
{
    public class OrderRepository : InMemoryRepository<OrderRequest>
    {
        public OrderRepository(LibraryMatanContext _db) : base(_db)
        {

        }

        private List<OrderRequestViewModel> _myViewModelList { get; set; }
        public void InsertOrder(OrderRequestViewModel model)
        {
            OrderRequest newOrderRequest = model.ToOrderRequest();

            var allLines = GetAll();
            var requestExists = allLines.FirstOrDefault(x => x.FreeBookText.Contains(model.Name));
            if (requestExists == null)
                requestExists = allLines.FirstOrDefault(x => x.FreeBookText.Contains(model.Author));
            if (requestExists == null || requestExists.OrderRequestTypeId != model.ActionToDo)
            {
                TryInsert(newOrderRequest);
                return;
            }
            // else;
            return; // already exists;
        }

        public List<OrderRequestViewModel> GetAllViewModel()
        {
            if (_myViewModelList != null && _myViewModelList.Any() && _lastCached > DateTime.UtcNow.AddDays(-1))
                return _myViewModelList;
            //else
            var all = GetAll().Where(x => x.CreatedDateTime > DateTime.Now.AddMonths(-1))
                .Select(x => new OrderRequestViewModel()
                {
                    ActionToDo = x.OrderRequestTypeId,
                    MembershipId = x.MembershipId,
                    Name = x.FreeBookText,
                    Id = x.Id, 
                    StatusId = x.StatusId
                }).ToList();
            _myViewModelList = all;
            return all;
        }
    }
}
