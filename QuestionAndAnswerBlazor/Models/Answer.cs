using System;
using System.Collections.Generic;

namespace QuestionAndAnswerBlazor.Models;

public partial class Answer
{
    public int Id { get; set; }

    public string Answer1 { get; set; } = null!;

    public int QuestionId { get; set; }

    public int UserId { get; set; }

    public DateTime CreateDate { get; set; }

    public int UpVotes { get; set; }

    public int DownVotes { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Question Question { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
