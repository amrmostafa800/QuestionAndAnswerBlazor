using System.ComponentModel.DataAnnotations;

namespace QuestionAndAnswerBlazor.DTOs
{
    public class AddAnswerDTO
    {
        [Required]
        public string Answer { get; set; } = string.Empty;

        [Required]
        public int QuestionID { get; set; }
    }
}
