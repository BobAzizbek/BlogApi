namespace blogapi.Entities;
public class Comment
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [MaxLength(255)]
    public string Author { get; set; }

    [MaxLength(1024)]
    public string Content { get; set; }

    public Entities.ECommentState State { get; set; }

    public Guid PostId { get; set; }

    [Obsolete("Used only for Entities binding.", true)]
    public Comment() { }

    public Comment(string author, string content, ECommentState state, Guid postId)
    {
        Id = Guid.NewGuid();
        Author = author;
        Content = content;
        State = state;
        PostId = postId;
    }
}