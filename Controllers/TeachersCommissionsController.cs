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
    public class TeachersCommissionsController : Controller
    {
        private readonly DBRegistryContext _context;

        public TeachersCommissionsController(DBRegistryContext context)
        {
            _context = context;
        }

        // GET: TeachersCommissions
        public async Task<IActionResult> Index()
        {
            var dBRegistryContext = _context.TeachersCommissions.Include(t => t.Commission).Include(t => t.Teacher);
            return View(await dBRegistryContext.ToListAsync());
        }

        // GET: TeachersCommissions/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> selectCommissions = from c in _context.Commissions
                                                           select new SelectListItem
                                                           {
                                                               Value = c.Id.ToString(),
                                                               Text = c.HeadLastName + " " + c.HeadFirstName + " " + c.HeadFathersName
                                                           };

            IEnumerable<SelectListItem> selectTeachers = from t in _context.Teachers
                                                        select new SelectListItem
                                                        {
                                                            Value = t.Id.ToString(),
                                                            Text = t.LastName + " " + t.FirstName + " " + t.FathersName
                                                        };

            ViewData["CommissionId"] = new SelectList(selectCommissions, "Value", "Text");
            ViewData["TeacherId"] = new SelectList(selectTeachers, "Value", "Text");
            return View();
        }

        // POST: TeachersCommissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeacherId,CommissionId,DefenseDate")] TeachersCommission teachersCommission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teachersCommission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CommissionId"] = new SelectList(_context.Commissions, "Id", "HeadLastName", teachersCommission.CommissionId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "LastName", teachersCommission.TeacherId);
            return View(teachersCommission);
        }

        // GET: TeachersCommissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeachersCommissions == null)
            {
                return NotFound();
            }

            var teachersCommission = await _context.TeachersCommissions.FindAsync(id);
            if (teachersCommission == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> selectCommissions = from c in _context.Commissions
                                                           select new SelectListItem
                                                           {
                                                               Value = c.Id.ToString(),
                                                               Text = c.HeadLastName + " " + c.HeadFirstName + " " + c.HeadFathersName
                                                           };

            IEnumerable<SelectListItem> selectTeachers = from t in _context.Teachers
                                                        select new SelectListItem
                                                        {
                                                            Value = t.Id.ToString(),
                                                            Text = t.LastName + " " + t.FirstName + " " + t.FathersName
                                                        };

            ViewData["CommissionId"] = new SelectList(selectCommissions, "Value", "Text", teachersCommission.CommissionId);
            ViewData["TeacherId"] = new SelectList(selectTeachers, "Value", "Text", teachersCommission.TeacherId);
            return View(teachersCommission);
        }

        // POST: TeachersCommissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeacherId,CommissionId,DefenseDate")] TeachersCommission teachersCommission)
        {
            if (id != teachersCommission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teachersCommission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeachersCommissionExists(teachersCommission.Id))
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
            ViewData["CommissionId"] = new SelectList(_context.Commissions, "Id", "HeadLastName", teachersCommission.CommissionId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "LastName", teachersCommission.TeacherId);
            return View(teachersCommission);
        }

        // GET: TeachersCommissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeachersCommissions == null)
            {
                return NotFound();
            }

            var teachersCommission = await _context.TeachersCommissions
                .Include(t => t.Commission)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teachersCommission == null)
            {
                return NotFound();
            }

            return View(teachersCommission);
        }

        // POST: TeachersCommissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeachersCommissions == null)
            {
                return Problem("Entity set 'DBRegistryContext.TeachersCommissions'  is null.");
            }
            var teachersCommission = await _context.TeachersCommissions.FindAsync(id);
            if (teachersCommission != null)
            {
                _context.TeachersCommissions.Remove(teachersCommission);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeachersCommissionExists(int id)
        {
          return _context.TeachersCommissions.Any(e => e.Id == id);
        }
    }
}
