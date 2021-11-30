namespace blogapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postS;
    private readonly IMediaService _mediaS;
    private readonly BlogDbContext _context;

    public PostController(IPostService postS, IMediaService mediaS, BlogDbContext context)
    {
        _postS = postS;
        _mediaS = mediaS;
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync(PostModel post)
    {
        if (await _mediaS.ExistsAsync(post.MediaId))
        {
            return BadRequest($"MediasId with given ID: {post.MediaId} not found.");
        }
        var medias = await _mediaS.GetAllAsync(post.MediaId);
        var entity = new Post(
            headerImageId: post.HeaderImageId,
            title: post.Title,
            description: post.Description,
            content: post.Content,
            comments: null,
            medias: medias);

        var result = await _postS.InsertAsync(entity);
        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest();
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var posts = await _postS.GetAllAsync();
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

        return Ok(json);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
        => Ok(await _postS.GetAsync(id));

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute] Guid id, PostModel post)
    {
        if (!await _postS.ExistsAsync(id))
        {
            return BadRequest($"Not found.");
        }
        var medias = await _mediaS.GetAllAsync(post.MediaId);
        var entity = new Post(
            headerImageId: post.HeaderImageId,
            title: post.Title,
            description: post.Description,
            content: post.Content,
            comments: null,
            medias: medias);

        entity.Id = id;

        var result = await _postS.UpdateAsync(entity);

        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
        => Ok(await _postS.DeleteAsync(id));
}