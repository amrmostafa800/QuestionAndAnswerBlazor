using System.ComponentModel.DataAnnotations;

namespace QuestionAndAnswer.DTOs
{
    public class AddAnswerDTO
    {
        [Required]
        public string Answer { get; set; } = string.Empty;

        [Required]
        public int QuestionID { get; set; }

        [Required]
        public int UserID { get; set; }
    }
}
