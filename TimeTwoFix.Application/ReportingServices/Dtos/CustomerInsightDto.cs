using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.ReportingServices.Dtos
{
    public class CustomerInsightDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        // Presentation-friendly values
        public int TotalVisits { get; set; }
        public string TotalSpend { get; set; } = string.Empty;   // formatted as currency
        public bool IsRepeatCustomer { get; set; }
        public string AverageInvoice { get; set; } = string.Empty; // formatted as currency
    }


}
