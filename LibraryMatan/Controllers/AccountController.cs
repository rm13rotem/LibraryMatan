using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryMatan.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMatan.Controllers
{
    public class AccountController : Controller
    {
        private InMemoryRepository<MembershipUser> repository;
        public AccountController(LibraryMatanContext context)
        {
            repository = new InMemoryRepository<MembershipUser>(context);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(MembershipUser model)
        {
            var existingUser = repository.GetAll().FirstOrDefault(x => x.UserIdentityNumber == model.UserIdentityNumber);
            if (existingUser != null)
                return RedirectToAction("CreateAndDo", "Books", existingUser);
            return View();
        }
    }
}
