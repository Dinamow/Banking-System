namespace Banking_System.Application.Account.DTOs
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public required string AccountNumber { get; set; }
        public required string AccountType { get; set; }
        public int Balance { get; set; }
        public int OverDraftLimit { get; set; }
        public float IntrestRate { get; set; }
        public decimal AccumulatedInterest { get; set; }
        public DateTime LastInterestCalculationDate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public required string UserId { get; set; }
    }
}
