using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegistryWebApplication.Models;
using ClosedXML.Excel;

namespace RegistryWebApplication.Controllers
{
    public class StudentsController : Controller
    {
        private readonly DBRegistryContext _context;

        public StudentsController(DBRegistryContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
              return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,FathersName,Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LastName,FirstName,FathersName,Email")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'DBRegistryContext.Students'  is null.");
            }

            var student = await _context.Students.FindAsync(id);

            var studentWork = await _context.Works.FirstOrDefaultAsync(t => t.StudentId == id);

            if (studentWork != null && student.Id == studentWork.StudentId)
            {
                ViewBag.Message = string.Format("Sorry, but this student has a work!");
                return View(student);
            }

            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if(fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                var students = await _context.Students.ToListAsync();
                                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                {
                                    try
                                    {
                                        Student student = new Student();
                                        student.LastName = row.Cell(1).Value.ToString();
                                        student.FirstName = row.Cell(2).Value.ToString();
                                        student.FathersName = row.Cell(3).Value.ToString();
                                        student.Email = row.Cell(4).Value.ToString();

                                        if (string.IsNullOrEmpty(student.LastName) || string.IsNullOrEmpty(student.FirstName) ||
                                            string.IsNullOrEmpty(student.FathersName) || string.IsNullOrEmpty(student.Email))
                                        {
                                            throw new NullReferenceException("All data fields must be filled. Action has terminated!");
                                        }

                                        foreach (var currStudent in students)
                                        {
                                            if (currStudent.LastName == student.LastName)
                                            {
                                                if (currStudent.FirstName == student.FirstName)
                                                {
                                                    if (currStudent.FathersName == student.FathersName)
                                                    {
                                                        throw new Exception($"{student.LastName} {student.FirstName} {student.FathersName} is already exist. Action has terminated!");
                                                    }
                                                }
                                            }
                                        }

                                        _context.Students.Add(student);
                                    }
                                    catch (NullReferenceException n)
                                    {
                                        ViewBag.Message = n.Message;
                                        return View();
                                    }
                                    catch (Exception e)
                                    {
                                        ViewBag.Message = e.Message;
                                        return View();
                                    }
                                }
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var students = _context.Students.ToList();

                var worksheet = workbook.Worksheets.Add("Students");

                worksheet.Cell("A1").Value = "Last name";
                worksheet.Cell("B1").Value = "First name";
                worksheet.Cell("C1").Value = "Fathers name";
                worksheet.Cell("D1").Value = "Email";
                worksheet.Row(1).Style.Font.Bold = true;

                for (int i = 0; i < students.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = students[i].LastName;
                    worksheet.Cell(i + 2, 2).Value = students[i].FirstName;
                    worksheet.Cell(i + 2, 3).Value = students[i].FathersName;
                    worksheet.Cell(i + 2, 4).Value = students[i].Email;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.speadsheetml.sheet")
                    {
                        FileDownloadName = $"RegistryStudents_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }

        private bool StudentExists(int id)
        {
          return _context.Students.Any(e => e.Id == id);
        }
    }
}
