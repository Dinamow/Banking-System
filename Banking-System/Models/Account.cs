namespace Banking_System.Models
{
    public class Account
    {
        public int Id { get; set; }
        public required string AccountNumber { get; set; }
        public required string AccountType { get; set; }
        public int Balance { get; set; }
        public int OverDraftLimit { get; set; }
        public float IntrestRate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
