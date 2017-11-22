using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class User
    {
        public User()
        {
            PaymentMethods = new List<PaymentMethod>();
        }

        [Key]
        public int UserId { get; set; }

        [MaxLength(80)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(25)]
        public string Password { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}
