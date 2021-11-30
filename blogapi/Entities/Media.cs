namespace blogapi.Entities;
public class Media
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [MaxLength(55)]
    public string ContentType { get; set; }

    [MaxLength(3145728)]
    public byte[] Data { get; set; }

    [Obsolete("Used only for Entities binding.", true)]
    public Media() { }

    public Media(string contentType, byte[] data)
    {
        Id = Guid.NewGuid();
        ContentType = contentType;
        Data = data;
    }
}