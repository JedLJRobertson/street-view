using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using street.Dto;
using street.Filters;
using street.Models;
using System.Runtime.Serialization.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace street.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly string _recognizerService;
        private readonly IHttpClientFactory _clientFactory;

        public ReportsController(MyContext context, IHttpClientFactory clientFactory)
        {
            _context = context;

            if (_context.Reports.Count() == 0)
            {
                InitExampleDb();
            }

            _recognizerService = "http://localhost:8080/?country_code=US";

            _clientFactory = clientFactory;
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

        [ServiceFilter(typeof(AuthFilter))]
        [HttpPost("recognize")]
        public async Task<ActionResult<Object>> RecognizePlate(IFormFile file)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _recognizerService);
            request.Content = new StreamContent(file.OpenReadStream());

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            var serializer = new DataContractJsonSerializer(typeof(ALPRReturnDto));

            var result = serializer.ReadObject(await response.Content.ReadAsStreamAsync());

            return serializer.ReadObject(await response.Content.ReadAsStreamAsync());
        }
    }
}
