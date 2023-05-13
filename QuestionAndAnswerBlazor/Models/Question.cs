using System;
using System.Collections.Generic;

namespace QuestionAndAnswerBlazor.Models;

public partial class Question
{
    public int Id { get; set; }

    public string Question1 { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual User User { get; set; } = null!;
}
