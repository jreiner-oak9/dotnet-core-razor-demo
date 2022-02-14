using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Razor.ASPNET.Controllers
{
    [Route("api/[controller]")]
    public class EmailSummary : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = Razor.Library.Models.EmailSummary.GetMockData();
            return View("~/Views/EmailSummary.cshtml", model);
        }
    }
}
