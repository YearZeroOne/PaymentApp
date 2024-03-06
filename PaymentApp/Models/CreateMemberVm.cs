namespace PaymentApp.Models
{
	public class CreateMemberVm
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		#nullable enable
		public string? Image { get; set; }
		public bool IsActive { get; set; }
		public int Id { get; set; }
	}
}
