using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.ReportingServices.Dtos
{
    public class PaymentAgingDto
    {
        public string AgingBucket { get; set; } = string.Empty; // e.g. "0-30 days"
        public decimal AmountDue { get; set; }
        public int WorkOrderId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public int DaysOutstanding { get; set; }
    }


}
