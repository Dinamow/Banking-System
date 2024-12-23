using System.ComponentModel.DataAnnotations;

namespace Banking_System.Core.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public required string AccountNumber { get; set; }
        [Required]
        public required string AccountType { get; set; }
        public int Balance { get; set; }
        public int OverDraftLimit { get; set; }
        public float IntrestRate { get; set; }
        public decimal AccumulatedInterest { get; set; }
        public DateTime LastInterestCalculationDate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
