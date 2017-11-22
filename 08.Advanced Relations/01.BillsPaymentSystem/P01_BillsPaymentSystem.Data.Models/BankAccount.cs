using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }
        public decimal Balance { get; set; }

        [MaxLength(50)]
        public string BankName { get; set; }

        [MaxLength(20)]
        public string SwiftCode { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            this.Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }
    }

}
