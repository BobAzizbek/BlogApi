
namespace blogapi.Entities;
public class Post
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public Guid HeaderImageId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }

    [MaxLength(1024)]
    public string Content { get; set; }

    public uint Viewed { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset ModifiedAt { get; set; }

    public ICollection<Comment> Comments { get; set; }

    public ICollection<Media> Medias { get; set; }

    [Obsolete("Used only for Entities binding.", true)]
    public Post() { }

    public Post(Guid headerImageId, string title, string description, string content, ICollection<Comment> comments, ICollection<Media> medias)
    {
        Id = Guid.NewGuid();
        HeaderImageId = headerImageId;
        Title = title;
        Description = description;
        Content = content;
        Viewed = 0;
        CreatedAt = DateTimeOffset.UtcNow;
        ModifiedAt = CreatedAt;
        Comments = comments;
        Medias = medias;
    }
}