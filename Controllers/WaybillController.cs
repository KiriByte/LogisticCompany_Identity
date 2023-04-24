using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LogisticCompany_Identity.Data;
using LogisticCompany_Identity.Models;
using Microsoft.AspNetCore.Authorization;

namespace LogisticCompany_Identity.Controllers
{
    [Authorize(Roles = "admin,employee")]
    public class WaybillController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WaybillController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Waybill
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Waybills.Include(w => w.Car).Include(w => w.FreightRequest);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Waybill/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Waybills == null)
            {
                return NotFound();
            }

            var waybill = await _context.Waybills
                .Include(w => w.Car)
                .Include(w => w.FreightRequest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waybill == null)
            {
                return NotFound();
            }

            return View(waybill);
        }

        // GET: Waybill/Create
        [Route("/Waybill/Create/{id?}")]
        public IActionResult Create(int? id)
        {
            if (id.HasValue)
            {
                ViewData["FreightRequestId"] =
                    new SelectList(_context.FreightRequests.Where(fr => fr.Id == id.Value), "Id", "Id");
            }
            else
            {
                ViewData["FreightRequestId"] = new SelectList(_context.FreightRequests, "Id", "Id");
            }

            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id");
            return View();
        }

        // POST: Waybill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWaybill(
            [Bind("Id,CarId,FreightRequestId,DepartureTime,ArrivalTime,DriverName")]
            Waybill waybill)
        {
            _context.Add(waybill);
            await _context.SaveChangesAsync();


            var freightRequest = await _context.FreightRequests.FindAsync(waybill.FreightRequestId);


            freightRequest.Status = "Accepted";
            _context.Update(freightRequest);
            await _context.SaveChangesAsync();

            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", waybill.CarId);
            ViewData["FreightRequestId"] =
                new SelectList(_context.FreightRequests, "Id", "Id", waybill.FreightRequestId);
            return RedirectToAction("Index");
        }

        // GET: Waybill/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Waybills == null)
            {
                return NotFound();
            }

            var waybill = await _context.Waybills.FindAsync(id);
            if (waybill == null)
            {
                return NotFound();
            }

            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", waybill.CarId);
            ViewData["FreightRequestId"] =
                new SelectList(_context.FreightRequests, "Id", "Id", waybill.FreightRequestId);
            return View(waybill);
        }

        // POST: Waybill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,CarId,FreightRequestId,DepartureTime,ArrivalTime,DriverName")]
            Waybill waybill)
        {
            if (id != waybill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(waybill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaybillExists(waybill.Id))
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

            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", waybill.CarId);
            ViewData["FreightRequestId"] =
                new SelectList(_context.FreightRequests, "Id", "Id", waybill.FreightRequestId);
            return View(waybill);
        }

        // GET: Waybill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Waybills == null)
            {
                return NotFound();
            }

            var waybill = await _context.Waybills
                .Include(w => w.Car)
                .Include(w => w.FreightRequest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waybill == null)
            {
                return NotFound();
            }

            return View(waybill);
        }

        // POST: Waybill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Waybills == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Waybills'  is null.");
            }

            var waybill = await _context.Waybills.FindAsync(id);
            if (waybill != null)
            {
                _context.Waybills.Remove(waybill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WaybillExists(int id)
        {
            return (_context.Waybills?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}