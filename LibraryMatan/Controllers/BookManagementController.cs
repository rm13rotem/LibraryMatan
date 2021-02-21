using LibraryMatan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryMatan.Controllers
{
    public class BookManagementController : Controller
    {
        InMemoryRepository<Book> repository;
        LibraryMatanContext _db;
        public BookManagementController(LibraryMatanContext db)
        {
            repository = new InMemoryRepository<Book>(db);
            _db = db;
        }
        public IActionResult VoteUpSingleBook(Book newBookOrderSelected)
        {
            var book = repository.GetAll().FirstOrDefault(x => x.Id == newBookOrderSelected.Id);
            if (book != null)
                book.RequestedTimes++;
            repository.SaveChanges();
            return RedirectToAction("BeingProcessed", "Books", null);
        }

        public IActionResult Refresh()
        {
            repository.RefreshIfStale(true);

            return RedirectToAction("Index", "Books");
        }
    }
}
