using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Razor.Library.Models;
using System.IO;
using Razor.Templating.Core;
using System.Text;

namespace Razor.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            // DI
            var services = new ServiceCollection();
            services.AddRazorTemplating();
            _ = services.BuildServiceProvider();

            // build model to bind to view
            var model = EmailSummary.GetMockData();

            // render view and save to filesystem
            var html = RazorTemplateEngine.RenderAsync("~/Views/EmailSummary.cshtml", model).Result;
            using (var fs = new FileStream(".\\out.html", FileMode.Create))
            {
                var bytes = Encoding.ASCII.GetBytes(html);
                fs.Write(bytes);
            }
        }
    }
}
