using System.ComponentModel.DataAnnotations;

namespace QuestionAndAnswer.DTOs
{
    public class VoteDTO
    {
        [Required]
        public int AnswerID { get; set; }

        [Required]
        public bool isUpVote { get; set; }
    }
}
