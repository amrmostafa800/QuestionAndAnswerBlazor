﻿using System.ComponentModel.DataAnnotations;

namespace QuestionAndAnswer.DTOs
{
    public class EditAnswerDTO
    {
        [Required]
        public int AnswerID { get; set; }

        [Required]
        public string NewAnswer { get; set; } = string.Empty;
    }
}
