using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gurleenProject.Data;
using gurleenProject.Models;

namespace gurleenProject.Controllers
{
    public class StoreExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StoreExpenses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StoreExpense.Include(s => s.Store);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StoreExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeExpense = await _context.StoreExpense
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.ExpenseId == id);
            if (storeExpense == null)
            {
                return NotFound();
            }

            return View(storeExpense);
        }

        // GET: StoreExpenses/Create
        public IActionResult Create()
        {
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "Description");
            return View();
        }

        // POST: StoreExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseId,StoreId,StoreName,TotalExpense,Description")] StoreExpense storeExpense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeExpense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "Description", storeExpense.StoreId);
            return View(storeExpense);
        }

        // GET: StoreExpenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeExpense = await _context.StoreExpense.FindAsync(id);
            if (storeExpense == null)
            {
                return NotFound();
            }
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "Description", storeExpense.StoreId);
            return View(storeExpense);
        }

        // POST: StoreExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseId,StoreId,StoreName,TotalExpense,Description")] StoreExpense storeExpense)
        {
            if (id != storeExpense.ExpenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeExpense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExpenseExists(storeExpense.ExpenseId))
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
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "Description", storeExpense.StoreId);
            return View(storeExpense);
        }

        // GET: StoreExpenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeExpense = await _context.StoreExpense
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.ExpenseId == id);
            if (storeExpense == null)
            {
                return NotFound();
            }

            return View(storeExpense);
        }

        // POST: StoreExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storeExpense = await _context.StoreExpense.FindAsync(id);
            _context.StoreExpense.Remove(storeExpense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExpenseExists(int id)
        {
            return _context.StoreExpense.Any(e => e.ExpenseId == id);
        }
    }
}
