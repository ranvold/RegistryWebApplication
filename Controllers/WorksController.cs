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
    public class WorksController : Controller
    {
        private readonly DBRegistryContext _context;

        public WorksController(DBRegistryContext context)
        {
            _context = context;
        }

        // GET: Works
        public async Task<IActionResult> Index()
        {
            var dBRegistryContext = _context.Works.Include(w => w.Classroom).Include(w => w.Commission).Include(w => w.Student).Include(w => w.Teacher);
            return View(await dBRegistryContext.ToListAsync());
        }

        // GET: Works/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Works == null)
            {
                return NotFound();
            }

            var work = await _context.Works
                .Include(w => w.Classroom)
                .Include(w => w.Commission)
                .Include(w => w.Student)
                .Include(w => w.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        // GET: Works/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> selectCommissions = from c in _context.Commissions
                                                            select new SelectListItem
                                                            {
                                                                Value = c.Id.ToString(),
                                                                Text = c.HeadLastName + " " + c.HeadFirstName + " " + c.HeadFathersName
                                                            };
            IEnumerable<SelectListItem> selectStudents = from s in _context.Students
                                                            select new SelectListItem
                                                            {
                                                                Value = s.Id.ToString(),
                                                                Text = s.LastName + " " + s.FirstName + " " + s.FathersName
                                                            };
            IEnumerable<SelectListItem> selectTeachers = from t in _context.Teachers
                                                         select new SelectListItem
                                                         {
                                                             Value = t.Id.ToString(),
                                                             Text = t.LastName + " " + t.FirstName + " " + t.FathersName
                                                         };

            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Number");
            ViewData["CommissionId"] = new SelectList(selectCommissions, "Value", "Text");
            ViewData["StudentId"] = new SelectList(selectStudents, "Value", "Text");
            ViewData["TeacherId"] = new SelectList(selectTeachers, "Value", "Text");
            return View();
        }

        // POST: Works/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Mark,StudentId,TeacherId,CommissionId,ClassroomId")] Work work)
        {
            if (ModelState.IsValid)
            {
                _context.Add(work);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Number", work.ClassroomId);
            ViewData["CommissionId"] = new SelectList(_context.Commissions, "Id", "HeadLastName", work.CommissionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "LastName", work.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "LastName", work.TeacherId);
            return View(work);
        }

        // GET: Works/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Works == null)
            {
                return NotFound();
            }

            var work = await _context.Works.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> selectCommissions = from c in _context.Commissions
                                                            select new SelectListItem
                                                            {
                                                                Value = c.Id.ToString(),
                                                                Text = c.HeadLastName + " " + c.HeadFirstName + " " + c.HeadFathersName
                                                            };
            IEnumerable<SelectListItem> selectStudents = from s in _context.Students
                                                         select new SelectListItem
                                                         {
                                                             Value = s.Id.ToString(),
                                                             Text = s.LastName + " " + s.FirstName + " " + s.FathersName
                                                         };
            IEnumerable<SelectListItem> selectTeachers = from t in _context.Teachers
                                                         select new SelectListItem
                                                         {
                                                             Value = t.Id.ToString(),
                                                             Text = t.LastName + " " + t.FirstName + " " + t.FathersName
                                                         };

            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Number", work.ClassroomId);
            ViewData["CommissionId"] = new SelectList(selectCommissions, "Value", "Text", work.CommissionId);
            ViewData["StudentId"] = new SelectList(selectStudents, "Value", "Text", work.StudentId);
            ViewData["TeacherId"] = new SelectList(selectTeachers, "Value", "Text", work.TeacherId);
            return View(work);
        }

        // POST: Works/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Mark,StudentId,TeacherId,CommissionId,ClassroomId")] Work work)
        {
            if (id != work.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(work);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkExists(work.Id))
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
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Number", work.ClassroomId);
            ViewData["CommissionId"] = new SelectList(_context.Commissions, "Id", "HeadLastName", work.CommissionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "LastName", work.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "LastName", work.TeacherId);
            return View(work);
        }

        // GET: Works/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Works == null)
            {
                return NotFound();
            }

            var work = await _context.Works
                .Include(w => w.Classroom)
                .Include(w => w.Commission)
                .Include(w => w.Student)
                .Include(w => w.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        // POST: Works/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Works == null)
            {
                return Problem("Entity set 'DBRegistryContext.Works'  is null.");
            }
            var work = await _context.Works.FindAsync(id);
            if (work != null)
            {
                _context.Works.Remove(work);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkExists(int id)
        {
          return _context.Works.Any(e => e.Id == id);
        }
    }
}
