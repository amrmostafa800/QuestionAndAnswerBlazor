namespace QuestionAndAnswerBlazor.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int? AnswerId { get; set; }

    public int? ParentId { get; set; }

    public string Comment1 { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual Answer? Answer { get; set; }

    public virtual ICollection<Comment> InverseParent { get; set; } = new List<Comment>();

    public virtual Comment? Parent { get; set; }

    public virtual User User { get; set; } = null!;
}
