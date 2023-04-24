using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LogisticCompany_Identity.Data;
using LogisticCompany_Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace LogisticCompany_Identity.Controllers
{
    public class FreightRequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public FreightRequestController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: FreightRequest
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FreightRequests.Include(f => f.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FreightRequest/Create
        public IActionResult Create()
        {
            var cities = _context.Cities.Select(x => x.Name).ToList();
            ViewBag.Cities = new SelectList(cities);
            return View();
        }

        // POST: FreightRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("DepartureCity,TargetCity,Weight,RequestDate")] FreightRequest freightRequest)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            freightRequest.User = currentUser;
            freightRequest.UserId = currentUser.Id;
            freightRequest.Status = "Pending";
            //if (ModelState.IsValid)
            {
                _context.Add(freightRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexForUsers");
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", freightRequest.UserId);
            return View(freightRequest);
        }

        // GET: FreightRequest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FreightRequests == null)
            {
                return NotFound();
            }

            var freightRequest = await _context.FreightRequests
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (freightRequest == null)
            {
                return NotFound();
            }

            return View(freightRequest);
        }

        // POST: FreightRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FreightRequests == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FreightRequests'  is null.");
            }

            var freightRequest = await _context.FreightRequests.FindAsync(id);
            if (freightRequest != null)
            {
                _context.FreightRequests.Remove(freightRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FreightRequestExists(int id)
        {
            return (_context.FreightRequests?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> Accept(int id)
        {
            if (id == null || _context.FreightRequests == null)
            {
                return NotFound();
            }

            var freightRequest = await _context.FreightRequests.FindAsync(id);
            if (freightRequest == null)
            {
                return NotFound();
            }

            freightRequest.Status = "Accepted";
            _context.Update(freightRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Decline(int id)
        {
            if (id == null || _context.FreightRequests == null)
            {
                return NotFound();
            }

            var freightRequest = await _context.FreightRequests.FindAsync(id);
            if (freightRequest == null)
            {
                return NotFound();
            }

            freightRequest.Status = "Decline";
            _context.Update(freightRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> IndexForUsers()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var requests = _context.FreightRequests
                .Where(fr => fr.UserId == currentUser.Id)
                .ToList();
            return View(requests);
        }

        public async Task<IActionResult> Cancel(int id)
        {
            if (id == null || _context.FreightRequests == null)
            {
                return NotFound();
            }

            var freightRequest = await _context.FreightRequests.FindAsync(id);
            if (freightRequest == null)
            {
                return NotFound();
            }

            freightRequest.Status = "Cancelled by User";
            _context.Update(freightRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexForUsers");
        }
    }
}