namespace blogapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromForm] MediaModel media)
    {
        var images = media.Data.Select(f =>
        {
            using var stream = new MemoryStream();
            f.CopyTo(stream);
            return new Media(contentType: f.ContentType, data: stream.ToArray());
        }).ToList();

        var result = await _mediaService.InsertAsync(images);
        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest();
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var medias = await _mediaService.GetAllAsync();
        var json = medias.Select(m => new
        {
            Id = m.Id,
            ContentType = m.ContentType
        });

        return Ok(json);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var file = await _mediaService.GetAsync(id);
        var stream = new MemoryStream(file.Data);
        return File(stream, file.ContentType);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _mediaService.DeleteAsync(id);
        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest();
    }
}