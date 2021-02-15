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
    public class VerifiedBookTitlesController : Controller
    {
        private readonly LibraryMatanContext _context;

        public VerifiedBookTitlesController(LibraryMatanContext context)
        {
            _context = context;
        }

        // GET: VerifiedBookTitles
        public async Task<IActionResult> Index()
        {
            return View(await _context.VerifiedBookTitle.ToListAsync());
        }

        // GET: VerifiedBookTitles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verifiedBookTitle = await _context.VerifiedBookTitle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verifiedBookTitle == null)
            {
                return NotFound();
            }

            return View(verifiedBookTitle);
        }

        // GET: VerifiedBookTitles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VerifiedBookTitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CreatedDateTime,Id")] VerifiedBookTitle verifiedBookTitle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verifiedBookTitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(verifiedBookTitle);
        }

        // GET: VerifiedBookTitles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verifiedBookTitle = await _context.VerifiedBookTitle.FindAsync(id);
            if (verifiedBookTitle == null)
            {
                return NotFound();
            }
            return View(verifiedBookTitle);
        }

        // POST: VerifiedBookTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,CreatedDateTime,Id")] VerifiedBookTitle verifiedBookTitle)
        {
            if (id != verifiedBookTitle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verifiedBookTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerifiedBookTitleExists(verifiedBookTitle.Id))
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
            return View(verifiedBookTitle);
        }

        // GET: VerifiedBookTitles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verifiedBookTitle = await _context.VerifiedBookTitle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verifiedBookTitle == null)
            {
                return NotFound();
            }

            return View(verifiedBookTitle);
        }

        // POST: VerifiedBookTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var verifiedBookTitle = await _context.VerifiedBookTitle.FindAsync(id);
            _context.VerifiedBookTitle.Remove(verifiedBookTitle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VerifiedBookTitleExists(int id)
        {
            return _context.VerifiedBookTitle.Any(e => e.Id == id);
        }
    }
}
