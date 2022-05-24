using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistryWebApplication.Models;

namespace RegistryWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class BarChartsController : ControllerBase
    {
        private readonly DBRegistryContext _context;

        public BarChartsController(DBRegistryContext context)
        {
            _context = context;
        }

        [HttpGet("JsonDataBarChart")]
        public JsonResult JsonDataBarChart()
        {

            var totalWorks = _context.Works.Count();
            var defensedWorks = _context.Works.Count(d => d.Mark >= 60);

            List<object> works = new List<object>();
            works.Add(new[] { "Works", "Total", "Defensed" });

            works.Add(new object[] { "Works", totalWorks, defensedWorks });

            return new JsonResult(works);
        }
    }
}
