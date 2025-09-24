using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Core.Interfaces.Repositories.ReportingModels
{
    public class PaymentAgingResult
    {
        public string AgingBucket { get; set; } = string.Empty; // e.g. "0-30 days"
        public decimal AmountDue { get; set; }
        public int WorkOrderId { get; set; }
        public string ClientName { get; set; }
        public int DaysOutstanding { get; set; }
    }

}
