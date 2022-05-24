using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistryWebApplication.Models;

namespace RegistryWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly DBRegistryContext _context;

        public ChartsController(DBRegistryContext context)
        {
            _context = context;
        }

        [HttpGet("JsonDataChart")]
        public JsonResult JsonDataChart()
        {
            var teachers = _context.Teachers.ToList();
            List<object> teacherWorks = new List<object>();
            teacherWorks.Add(new[] { "Teacher", "Works" });

            foreach (var t in teachers)
            {
                var workCount = _context.Works.Count(w => w.TeacherId == t.Id);

                teacherWorks.Add(new object[] { t.LastName + " " + t.FirstName + " " + t.FathersName, workCount });
            }
            return new JsonResult(teacherWorks);
        }


    }

}
