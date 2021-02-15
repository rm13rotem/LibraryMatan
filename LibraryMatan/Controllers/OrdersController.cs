using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryMatan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMatan.Controllers
{
    public class OrdersController : Controller
    {
        private readonly LibraryMatanContext _context;
        private readonly OrderRepository orderRepository;

        public OrdersController(LibraryMatanContext context)
        {
            _context = context;
            orderRepository = new OrderRepository(context);
        }

        // GET: Books
        public IActionResult Index()
        {
            List<OrderRequestViewModel> newOrders = orderRepository.GetAllViewModel();
            return View(newOrders);
        }
        // GET: Books
        public IActionResult Authorize(int id)
        {
            var exist = orderRepository.GetByID(id);
            exist.StatusId = 3;
            return View("Index");
        }
        // GET: Books
        public IActionResult Deny(int id)
        {
            var exist = orderRepository.GetByID(id);
            exist.StatusId = 4;
            return View("Index");
        }
        // GET: Books
        public IActionResult Discuss(int id)
        {
            var exist = orderRepository.GetByID(id);
            exist.StatusId = 2;

            return View("Index");
        }
    }
}
