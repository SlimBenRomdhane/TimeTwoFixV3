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
        public decimal TotalSpend { get; set; }    // formatted as currency
        public bool IsRepeatCustomer { get; set; }
        public decimal AverageInvoice { get; set; } // formatted as currency
    }


}
