using System.Collections.Generic;

namespace PaymentApp.Models
{
	public class StatVm
	{

		public StatFilter StatFilter { get; set; }

		public int? Id { get; set; }
		public decimal CurrentAmount { get; set; }
		public decimal Payments { get; set; }
		public decimal Deposits { get; set; }
		public decimal AveragePayments { get; set; }
		public decimal AverageDeposits { get; set; }

		public List<MemberVm> Members { get; set; }

		public List<string> TopDepositMembers { get; set; } = new List<string>();
		public List<decimal> TopDeposits { get; set; } = new List<decimal>();

        public List<string> TopPaymentMembers { get; set; } = new List<string>();
        public List<decimal> TopPayments { get; set; } = new List<decimal>();
    }
}
