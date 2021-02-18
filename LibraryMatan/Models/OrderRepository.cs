using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMatan.Models
{
    public class OrderRepository : InMemoryRepository<OrderRequest>
    {
        LibraryMatanContext db;
        public OrderRepository(LibraryMatanContext _db) : base(_db)
        {
            db = _db;
        }

        private List<OrderDisplayViewModel> _myViewModelList { get; set; }
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

        public List<OrderDisplayViewModel> GetAllViewModel()
        {
            if (_myViewModelList != null && _myViewModelList.Any() && _lastCached > DateTime.UtcNow.AddDays(-1))
                return _myViewModelList;

            //else
            InMemoryRepository<OrderRequestTypeDescription> descriptionsRepository = new InMemoryRepository<OrderRequestTypeDescription>(db);
            var descriptions = descriptionsRepository.GetAll();
            InMemoryRepository<MembershipUser> membersRepository = new InMemoryRepository<MembershipUser>(db);
            var members = membersRepository.GetAll();
            InMemoryRepository<OrderRequestStatusDescription> statusesRepository = new InMemoryRepository<OrderRequestStatusDescription>(db);
            var statuses = statusesRepository.GetAll();

            var all = GetAll().Where(x => x.CreatedDateTime > DateTime.Now.AddMonths(-1))
                .Select(x => new OrderDisplayViewModel()
                {
                    ActionToDo = descriptions.First(y=> y.Id == x.OrderRequestTypeId).DescriptionText,
                    MembershipId = x.MembershipId == 0 ? "כניסה ללא הרשאה" : members.First(y => y.Id == x.MembershipId).UserNameText,
                    Name = x.FreeBookText,
                    Id = x.Id, 
                    StatusId = statuses.First(y => y.Id == x.StatusId).DescriptionText
                }).ToList();
            _myViewModelList = all;
            return all;
        }
    }
}
