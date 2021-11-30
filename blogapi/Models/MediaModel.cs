namespace blogapi.Models;
public class MediaModel
{
    [Required]
    [MaxLength(3145728)]
    public IEnumerable<IFormFile> Data { get; set; }
}