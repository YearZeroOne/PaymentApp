using PaymentApp.Data;
using System.Collections.Generic;

namespace PaymentApp.Models
{
    public class PaymentOverviewVm
    {
        public FilterInfo Filter { get; set; }
        public List<MemberVm> Members{ get; set; }
        public List<PaymentVm> Payments { get; set; }
    }
}
