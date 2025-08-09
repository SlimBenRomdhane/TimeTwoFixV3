using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTwoFix.Core.Entities.ServiceManagement;

namespace TimeTwoFix.Application.ProvidedServicesService.Dtos
{
    public class CreateProvidedServiceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTime { get; set; }
        public decimal PricePerHour { get; set; }
        public int CategoryId { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Createdby { get; set; }

    }
}
