using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using street.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace street.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly MyContext _context;

        public ReportsController(MyContext context)
        {
            _context = context;

            if (_context.Reports.Count() == 0)
            {
                InitExampleDb();
            }
        }

        private void InitExampleDb()
        {
            _context.Reports.Add(new ReportItem { Name = "Example1", Comment = "test" });
            _context.Reports.Add(new ReportItem { Name = "Example2", Comment = "test again" });
            _context.Reports.Add(new ReportItem { Name = "Example3", Comment = "a comment" });
            _context.SaveChanges();
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ReportItem>>> GetReports()
        {
            var reports = await _context.Reports.ToListAsync();
            return reports;
        }

        [HttpGet("{id}")]
        public ActionResult<ReportItem> GetReport(long id)
        {
            var report = _context.Reports.Find(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }
    }
}
