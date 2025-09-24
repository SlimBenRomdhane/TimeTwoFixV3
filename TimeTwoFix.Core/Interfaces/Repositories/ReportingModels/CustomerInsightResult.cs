using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Core.Interfaces.Repositories.ReportingModels
{
    // Customer insights
    public class CustomerInsightResult
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int TotalVisits { get; set; }
        public decimal TotalSpend { get; set; }
        public bool IsRepeatCustomer => TotalVisits > 1;
        public decimal AverageInvoice { get; set; }
    }

}
