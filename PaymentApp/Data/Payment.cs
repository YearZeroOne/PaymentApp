using System;
using System.Collections.Generic;

#nullable disable

namespace PaymentApp.Data
{
    public partial class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
        public int MemberId { get; set; }
        public bool InOut { get; set; }

        public virtual Member Member { get; set; }
    }
}
