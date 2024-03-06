using PaymentApp.Data;
using System;
using System.Collections.Generic;

namespace PaymentApp.Models
{
	public class CreatePaymentVm
	{
		public decimal Amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public int MemberId { get; set; }
		public string Description { get; set; }
		public bool InOut { get; set; }


		public List<Member> Members { get; set; } = new List<Member>();
    }
}
