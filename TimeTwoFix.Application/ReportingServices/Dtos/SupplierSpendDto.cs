using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.ReportingServices.Dtos
{
    public class SupplierSpendDto
    {
        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public decimal TotalSpend { get; set; }   // raw value, formatting handled in ViewModel
        public int DeliveriesCount { get; set; }
    }

}
