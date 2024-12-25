namespace Banking_System.Application.Auth.DTOs
{
    public class AuthResponseDTO
    {
        public required string Username { get; set; }
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
