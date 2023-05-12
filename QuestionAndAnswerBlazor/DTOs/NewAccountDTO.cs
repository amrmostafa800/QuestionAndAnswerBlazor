using System.ComponentModel.DataAnnotations;

namespace QuestionAndAnswerBlazor.DTOs
{
    public sealed class NewAccountDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
