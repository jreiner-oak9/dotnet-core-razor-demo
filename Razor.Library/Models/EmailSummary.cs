using System;
using System.Collections.Generic;

namespace Razor.Library.Models
{
    public class EmailSummary
    {
        public string RecipientName { get; set; }
        public string TenantName { get; set; }
        public List<Project> Projects { get; set; }

        public static EmailSummary GetMockData()
        {
            return new EmailSummary
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
        }
    }

    public class Project
    {
        public string Name { get; set; }
        public List<ComplianceFramework> ComplianceFrameworks { get; set; }
        public DesignGapSummary DesignGapSummary { get; set; }
    }

    public class ComplianceFramework
    {
        public string Name { get; set; }
        public bool IsInCompliance { get; set; }
    }

    public class DesignGapSummary
    {
        public int Critical { get; set; }
        public int High { get; set; }
        public int Medium { get; set; }
        public int Low { get; set; }
    }
}
