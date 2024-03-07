using System.Collections.Generic;

namespace PaymentApp.Models
{
	public class StatVm
	{

		public StatFilter StatFilter { get; set; }
		public StatFilterObj StatFilterObj { get; set; }

		public int? Id { get; set; }
		public decimal CurrentAmount { get; set; }
		public decimal Payments { get; set; }
		public decimal Deposits { get; set; }
		public decimal AveragePayments { get; set; }
		public decimal AverageDeposits { get; set; }

		public decimal OverallCurrentAmount { get; set; }
		public decimal OverallPayments { get; set; }
		public decimal OverallDeposits { get; set; }
		public decimal OverallAveragePayments { get; set; }
		public decimal OverallAverageDeposits { get; set; }



		public List<MemberVm> Members { get; set; }

		public List<string> TopDepositMembers { get; set; } = new List<string>();
		public List<decimal> TopDeposits { get; set; } = new List<decimal>();

        public List<string> TopPaymentMembers { get; set; } = new List<string>();
        public List<decimal> TopPayments { get; set; } = new List<decimal>();


		public List<string> OverallTopDepositMembers { get; set; } = new List<string>();
		public List<decimal> OverallTopDeposits { get; set; } = new List<decimal>();

		public List<string> OverallTopPaymentMembers { get; set; } = new List<string>();
		public List<decimal> OverallTopPayments { get; set; } = new List<decimal>();
	}
}
