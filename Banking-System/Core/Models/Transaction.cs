using System.ComponentModel.DataAnnotations;

namespace Banking_System.Core.Models
{
    public class BankingTransaction
    {
        public int Id { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public required string TransactionType { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int TargetAccount { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
