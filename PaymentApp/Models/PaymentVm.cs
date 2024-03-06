using System;

namespace PaymentApp.Models
{
    public class PaymentVm
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool InOut { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public int MemberId { get; set; }
#nullable enable
        public string? Image { get; set; }

    }
}
