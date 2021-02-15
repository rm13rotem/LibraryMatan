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
    public class VerifiedAuthorsController : Controller
    {
        private readonly LibraryMatanContext _context;

        public VerifiedAuthorsController(LibraryMatanContext context)
        {
            _context = context;
        }

        // GET: VerifiedAuthors
        public async Task<IActionResult> Index()
        {
            return View(await _context.VerifiedAuthor.ToListAsync());
        }

        // GET: VerifiedAuthors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verifiedAuthor = await _context.VerifiedAuthor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verifiedAuthor == null)
            {
                return NotFound();
            }

            return View(verifiedAuthor);
        }

        // GET: VerifiedAuthors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VerifiedAuthors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,CreatedDateTime")] VerifiedAuthor verifiedAuthor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verifiedAuthor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(verifiedAuthor);
        }

        // GET: VerifiedAuthors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verifiedAuthor = await _context.VerifiedAuthor.FindAsync(id);
            if (verifiedAuthor == null)
            {
                return NotFound();
            }
            return View(verifiedAuthor);
        }

        // POST: VerifiedAuthors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,CreatedDateTime")] VerifiedAuthor verifiedAuthor)
        {
            if (id != verifiedAuthor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verifiedAuthor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerifiedAuthorExists(verifiedAuthor.Id))
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
            return View(verifiedAuthor);
        }

        // GET: VerifiedAuthors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verifiedAuthor = await _context.VerifiedAuthor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verifiedAuthor == null)
            {
                return NotFound();
            }

            return View(verifiedAuthor);
        }

        // POST: VerifiedAuthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var verifiedAuthor = await _context.VerifiedAuthor.FindAsync(id);
            _context.VerifiedAuthor.Remove(verifiedAuthor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VerifiedAuthorExists(int id)
        {
            return _context.VerifiedAuthor.Any(e => e.Id == id);
        }
    }
}
