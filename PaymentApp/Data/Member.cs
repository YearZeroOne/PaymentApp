using System;
using System.Collections.Generic;

#nullable disable

namespace PaymentApp.Data
{
    public partial class Member
    {
        public Member()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
