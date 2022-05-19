using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegistryWebApplication.Models;

namespace RegistryWebApplication.Controllers
{
    public class CommissionsController : Controller
    {
        private readonly DBRegistryContext _context;

        public CommissionsController(DBRegistryContext context)
        {
            _context = context;
        }

        // GET: Commissions
        public async Task<IActionResult> Index()
        {
              return View(await _context.Commissions.ToListAsync());
        }

        // GET: Commissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Commissions == null)
            {
                return NotFound();
            }

            var commission = await _context.Commissions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commission == null)
            {
                return NotFound();
            }

            return View(commission);
        }

        // GET: Commissions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Commissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeadLastName,HeadFirstName,HeadFathersName")] Commission commission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commission);
        }

        // GET: Commissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Commissions == null)
            {
                return NotFound();
            }

            var commission = await _context.Commissions.FindAsync(id);
            if (commission == null)
            {
                return NotFound();
            }
            return View(commission);
        }

        // POST: Commissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeadLastName,HeadFirstName,HeadFathersName")] Commission commission)
        {
            if (id != commission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommissionExists(commission.Id))
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
            return View(commission);
        }

        // GET: Commissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Commissions == null)
            {
                return NotFound();
            }

            var commission = await _context.Commissions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commission == null)
            {
                return NotFound();
            }

            return View(commission);
        }

        // POST: Commissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Commissions == null)
            {
                return Problem("Entity set 'DBRegistryContext.Commissions'  is null.");
            }
            var commission = await _context.Commissions.FindAsync(id);
            if (commission != null)
            {
                _context.Commissions.Remove(commission);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommissionExists(int id)
        {
          return _context.Commissions.Any(e => e.Id == id);
        }
    }
}
