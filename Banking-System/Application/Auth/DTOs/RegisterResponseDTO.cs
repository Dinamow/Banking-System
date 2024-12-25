namespace Banking_System.Application.Auth.DTOs
{
    public class RegisterResponseDTO
    {
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
