namespace blogapi.Models;
public class PostModel
{
    public Guid HeaderImageId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }

    [MaxLength(1024)]
    public string Content { get; set; }

    public IEnumerable<Guid> MediaId { get; set; }
}