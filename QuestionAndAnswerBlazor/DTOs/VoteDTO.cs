using System.ComponentModel.DataAnnotations;

namespace QuestionAndAnswerBlazor.DTOs
{
    public class VoteDTO
    {
        [Required]
        public int AnswerID { get; set; }

        [Required]
        public bool isUpVote { get; set; }
    }
}
