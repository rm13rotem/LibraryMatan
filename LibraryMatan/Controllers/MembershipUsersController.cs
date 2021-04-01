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
    public class MembershipUsersController : Controller
    {
        private readonly LibraryMatanContext _context;

        public MembershipUsersController(LibraryMatanContext context)
        {
            _context = context;
        }

        // GET: MembershipUsers
        public async Task<IActionResult> Index(UserSearchFilter filter)
        {
            return View(await _context.MembershipUser.ToListAsync());
        }

        // GET: MembershipUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipUser = await _context.MembershipUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipUser == null)
            {
                return NotFound();
            }

            return View(membershipUser);
        }

        // GET: MembershipUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MembershipUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MembershipUser membershipUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membershipUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(membershipUser);
        }

        // GET: MembershipUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipUser = await _context.MembershipUser.FindAsync(id);
            if (membershipUser == null)
            {
                return NotFound();
            }
            return View(membershipUser);
        }

        // POST: MembershipUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MembershipUser membershipUser)
        {
            if (id != membershipUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershipUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipUserExists(membershipUser.Id))
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
            return View(membershipUser);
        }

        // GET: MembershipUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipUser = await _context.MembershipUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipUser == null)
            {
                return NotFound();
            }

            return View(membershipUser);
        }

        // POST: MembershipUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membershipUser = await _context.MembershipUser.FindAsync(id);
            _context.MembershipUser.Remove(membershipUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipUserExists(int id)
        {
            return _context.MembershipUser.Any(e => e.Id == id);
        }
    }
}
