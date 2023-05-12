using System;
using System.Collections.Generic;

namespace QuestionAndAnswer.Models;

public partial class AnswersView
{
    public int Id { get; set; }

    public string Answer { get; set; } = null!;

    public int QuestionId { get; set; }

    public int UserId { get; set; }

    public DateTime CreateDate { get; set; }

    public int? Sort { get; set; }
}
