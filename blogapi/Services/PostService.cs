namespace blogapi.Services;
public class PostService : IPostService
{
    private readonly BlogDbContext _context;
    private readonly ILogger<PostService> _logger;
    private readonly IMediaService _mediaS;

    public PostService(BlogDbContext context, ILogger<PostService> logger, IMediaService mediaS)
    {
        _context = context;
        _logger = logger;
        _mediaS = mediaS;
    }
    public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id)
    {
        var post = await GetAsync(id);
        if (post is default(Post))
        {
            return (false, new Exception("Not found."));
        }
        try
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Post deleted in DB. ID: {post.Id}");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Deleting post in DB failed.\nError:{e.Message}", e);
            return (false, e);
        }
    }

    public Task<bool> ExistsAsync(Guid id)
        => _context.Posts.AnyAsync(p => p.Id == id);

    public Task<List<Post>> GetAllAsync()
        => _context.Posts.Include(p => p.Medias).Include(p => p.Comments).ToListAsync();

    public Task<Post> GetAsync(Guid id)
        => _context.Posts.Include(p => p.Comments).Include(p => p.Medias).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<Post>> GetJsonAsync()
    {
        var posts = await GetAllAsync();
        var json = posts.Select(p => new
        {
            Id = p.Id,
            HeaderImageId = p.HeaderImageId,
            Title = p.Title,
            Description = p.Description,
            Content = p.Content,
            Viewed = p.Viewed,
            CreatedAt = p.CreatedAt,
            ModifiedAt = p.ModifiedAt,
            Comments = p.Comments.Select(c => new
            {
                Id = c.Id,
                Author = c.Author,
                Content = c.Content,
                State = c.State,
                PostId = c.PostId
            }),
            Medias = p.Medias.Select(m => new
            {
                Id = m.Id,
                ContentType = m.ContentType
            }),
        });

        return posts;
    }

    public async Task<(bool IsSuccess, Exception Exception)> InsertAsync(Post post)
    {
        if (!await _mediaS.ExistsAsync(post.HeaderImageId))
        {
            return (false, new Exception("Not found."));
        }
        try
        {
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Post created in DB. ID: {post.Id}");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Creating post in DB failed.\nError:{e.Message}", e);
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception Exception)> UpdateAsync(Post post)
    {
        try
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Post updated in DB. ID: {post.Id}");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Updating post in DB failed.\nError:{e.Message}", e);
            return (false, e);
        }
    }
}