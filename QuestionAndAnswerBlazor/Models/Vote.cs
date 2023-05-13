using System;
using System.Collections.Generic;

namespace QuestionAndAnswerBlazor.Models;

public partial class Vote
{
    public int Id { get; set; }

    public int AnswerId { get; set; }

    public bool IsUpVote { get; set; }

    public int UserId { get; set; }

    public virtual Answer Answer { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
