using System;

namespace PaymentApp.Models
{
	public class ListPaymentsVm
	{
		public int Id { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public DateTime PaymentTime { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Image { get; set; }
		public bool InOut { get; set; }
	}
}
