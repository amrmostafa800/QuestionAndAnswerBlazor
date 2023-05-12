using System.ComponentModel.DataAnnotations;

namespace QuestionAndAnswer.DTOs
{
    public sealed class LoginAccountDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
