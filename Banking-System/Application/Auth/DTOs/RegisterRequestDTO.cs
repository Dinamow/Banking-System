namespace Banking_System.Application.Auth.DTOs
{
    public class RegisterRequestDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; } // Optional for login but required for registration
    }
}
