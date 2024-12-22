namespace Banking_System.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public required string TransactionType { get; set; }
        public int Amount { get; set; }
        public int TargetAccount { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
