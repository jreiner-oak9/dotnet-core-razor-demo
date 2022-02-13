using System;
using System.Collections.Generic;

namespace Razor.Library.Models
{
    public class EmailSummary
    {
        public string RecipientName { get; set; }
        public string TenantName { get; set; }
        public List<Project> Projects { get; set; }
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
