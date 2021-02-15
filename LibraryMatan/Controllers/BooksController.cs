using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMatan.Models;

namespace LibraryMatan.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryMatanContext _context;
        private readonly OrderRepository orderRepository;
        private readonly InMemoryRepository<Book> booksRepository;
        private readonly InMemoryRepository<Genre> genreRepository;

        public BooksController(LibraryMatanContext context)
        {
            _context = context;
            orderRepository = new OrderRepository(context);
            booksRepository = new InMemoryRepository<Book>(context);
            genreRepository = new InMemoryRepository<Genre>(context);
        }

        // GET: Books
        public  IActionResult Index(BookSearchFilter filter)
        {
            var newBooks = booksRepository.GetAll().ToList();
            if (!string.IsNullOrWhiteSpace(filter.Text))
                newBooks = newBooks.Where(x => x.Name.Contains(filter.Text)).ToList();
            else if (!string.IsNullOrWhiteSpace(filter.Author))
                newBooks = newBooks.Where(x => x.Author.Contains(filter.Author)).ToList();
            else if (filter.GenreId > 0)
                newBooks = newBooks.Where(x => x.GenreId == filter.GenreId).ToList();
            else newBooks = newBooks.Where(x => x.CreatedDateTime > DateTime.Now.AddMonths(-1)).ToList();
            filter.Result = newBooks;
            var genres = genreRepository.GetAll().ToList();
            genres.Add(new Genre() { Id = 0, GenreName = "ללא סינון" });
            ViewBag.GenreId = genres;
            return View(filter);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewBag.GenreId = genreRepository.GetAll();
            return View();
        }
        public async Task<IActionResult> CreateAndDo(MembershipUser membershipUser)
        {
            OrderRequestViewModel model = new OrderRequestViewModel();
            model.MembershipId = membershipUser.Id;
            ViewBag.MembershipId = new List<MembershipUser>() { membershipUser };
            ViewBag.GenreId = await _context.Genre.ToListAsync();
            ViewBag.ActionToDo = await _context.OrderRequestTypeDescription.ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAndDo(OrderRequestViewModel model)
        {
            if (model.ToBook().IsValid() && model.ActionToDo > 0)
            {
                // Insert book
                var newBook = model.ToBook();
                _context.Add(newBook);
                await _context.SaveChangesAsync();

                // Insert order
                orderRepository.InsertOrder(model);
                var entered = orderRepository.GetAll().First(x => x.FreeBookText == model.Name);
                entered.BookId = newBook.Id;
                orderRepository.TryUpdate(entered);
                return RedirectToAction("BeingProcessed");
            }
            ViewBag.GenreId = await _context.Genre.ToListAsync();
            ViewBag.ActionToDo = await _context.OrderRequestTypeDescription.ToListAsync();
            return View();
        }
        public IActionResult BeingProcessed()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,VerifiedNameId,VerifiedAuthorId,Author,Price,RequestedTimes,GenreId,CreatedDateTime")] Book book)
        {
            if (ModelState.IsValid && book.IsValid())
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,VerifiedNameId,VerifiedAuthorId,Author,Price,RequestedTimes,GenreId,CreatedDateTime")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
