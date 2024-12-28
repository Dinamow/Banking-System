namespace Banking_System.Application.Transaction.DTOs
{
    public class TransferDTO
    {
        public int FromAccount { get; set; }
        public decimal Amount { get; set; }
        public int ToAccount { get; set; }
    }
}
