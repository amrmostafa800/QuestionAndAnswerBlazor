using System.ComponentModel.DataAnnotations;

namespace QuestionAndAnswerBlazor.DTOs
{
    public class EditQuestionDTO
    {
        [Required]
        public int QuestionID { get; set; }

        [Required]
        public string NewQuestion { get; set; } = string.Empty;
    }
}
