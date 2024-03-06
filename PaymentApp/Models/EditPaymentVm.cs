using PaymentApp.Data;
using System;
using System.Collections.Generic;

namespace PaymentApp.Models
{
    public class EditPaymentVm
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime PaymentTime { get; set; }
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool InOut { get; set; }



        public List<Member> Members { get; set; } = new List<Member>();

    }
}
