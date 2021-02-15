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
        private readonly OrderRepository orderRepository = new OrderRepository();

        public BooksController(LibraryMatanContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var newBooks = await _context.Book.Where(x => x.CreatedDateTime > DateTime.Now.AddMonths(-1)).ToListAsync();
            return View(newBooks);
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
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.GenreId = await _context.Genre.ToListAsync();
            return View();
        }
        public async Task<IActionResult> CreateAndDo(OrderRequestViewModel model)
        {

            ViewBag.GenreId = await _context.Genre.ToListAsync();
            ViewBag.ActionToDo = await _context.OrderRequestTypeDescription.ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAndDo(Book book, int ActionToDo, int memberId)
        {
            if (book.IsValid() && ActionToDo > 0)
            {
                orderRepository.InsertOrder(book, ActionToDo, memberId);
                return RedirectToAction("BeingProcessed");
            }
            ViewBag.GenreId = await _context.Genre.ToListAsync();
            ViewBag.ActionToDo = await _context.OrderRequestTypeDescription.ToListAsync();
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
