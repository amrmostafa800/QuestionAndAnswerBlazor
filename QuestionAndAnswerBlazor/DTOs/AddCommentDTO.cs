using System.ComponentModel.DataAnnotations;

namespace QuestionAndAnswerBlazor.DTOs
{
    public class AddCommentDTO
    {
        [Required]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public int ParentOrAnswerID { get; set; }

        [Required]
        public bool isCommentOnAnswer { get; set; }

        [Required]
        public int UserID { get; set; }
    }
}
