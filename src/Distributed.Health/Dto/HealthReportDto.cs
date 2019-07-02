
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accolades.Maije.Distributed.Health
{
    public class HealthReportDto
    {
        /// <summary>
        /// Gets or sets the health status
        /// </summary>
        [Required]
        public HealthStatus Status { get; set; }

        public string Version { get; set; }

        public string ReleaseId { get; set; }

        public List<string> Notes { get; set; }

        public string Output { get; set; }

        public string Description { get; set; }

        public string ServiceId { get; set; }

        public Dictionary<string, ComponentReportDto> Details { get; set; }
    }
}
