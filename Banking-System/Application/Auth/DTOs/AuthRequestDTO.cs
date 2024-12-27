namespace Banking_System.Application.Auth.DTOs
{
    public class AuthRequestDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
