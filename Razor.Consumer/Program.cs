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
            var model = new EmailSummary
            {
                RecipientName = "John",
                TenantName = "Acorn Calculator",
                Projects = new List<Project>()
                {
                    new Project()
                    {
                        Name = "Hello World",
                        ComplianceFrameworks = new List<ComplianceFramework>()
                        {
                            new ComplianceFramework()
                            {
                                Name = "HITRUST",
                                IsInCompliance = false
                            },
                            new ComplianceFramework()
                            {
                                Name = "NIST",
                                IsInCompliance = true
                            }
                        },
                        DesignGapSummary = new DesignGapSummary()
                        {
                            Critical = 1
                        }
                    },
                    new Project()
                    {
                        Name = "Part Two",
                        ComplianceFrameworks = new List<ComplianceFramework>()
                        {
                            new ComplianceFramework()
                            {
                                Name = "PCI DSS",
                                IsInCompliance = false
                            },
                        },
                        DesignGapSummary = new DesignGapSummary()
                        {
                            High = 1,
                            Medium = 4,
                            Low = 3
                        }
                    }
                }
            };

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
