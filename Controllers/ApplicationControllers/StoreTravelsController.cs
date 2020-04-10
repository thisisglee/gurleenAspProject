using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gurleenProject.Data;
using gurleenProject.Models;

namespace gurleenProject.Controllers.ApplicationControllers
{
    public class StoreTravelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreTravelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StoreTravels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StoreTravel.Include(s => s.Store);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StoreTravels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeTravel = await _context.StoreTravel
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.TravelId == id);
            if (storeTravel == null)
            {
                return NotFound();
            }

            return View(storeTravel);
        }

        // GET: StoreTravels/Create
        public IActionResult Create()
        {
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "Description");
            return View();
        }

        // POST: StoreTravels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TravelId,StoreId,StoreName,LocationFrom,LocationTo,Description")] StoreTravel storeTravel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeTravel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "Description", storeTravel.StoreId);
            return View(storeTravel);
        }

        // GET: StoreTravels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeTravel = await _context.StoreTravel.FindAsync(id);
            if (storeTravel == null)
            {
                return NotFound();
            }
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "Description", storeTravel.StoreId);
            return View(storeTravel);
        }

        // POST: StoreTravels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TravelId,StoreId,StoreName,LocationFrom,LocationTo,Description")] StoreTravel storeTravel)
        {
            if (id != storeTravel.TravelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeTravel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreTravelExists(storeTravel.TravelId))
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
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "Description", storeTravel.StoreId);
            return View(storeTravel);
        }

        // GET: StoreTravels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeTravel = await _context.StoreTravel
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.TravelId == id);
            if (storeTravel == null)
            {
                return NotFound();
            }

            return View(storeTravel);
        }

        // POST: StoreTravels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storeTravel = await _context.StoreTravel.FindAsync(id);
            _context.StoreTravel.Remove(storeTravel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreTravelExists(int id)
        {
            return _context.StoreTravel.Any(e => e.TravelId == id);
        }
    }
}
