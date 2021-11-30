namespace blogapi.Models;
public class CommentModel
{
    public string Author { get; set; }

    public string Content { get; set; }

    public Models.ECommentState State { get; set; }

    public Guid Id { get; set; }
}