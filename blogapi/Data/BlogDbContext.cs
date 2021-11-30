namespace blogapi.Data;
public class BlogDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Media> Medias { get; set; }

    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options) { }
}