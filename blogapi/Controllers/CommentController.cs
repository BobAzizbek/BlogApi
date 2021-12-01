using System.Linq;
namespace blogapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentS;
    private readonly IPostService _postS;

    public CommentController(ICommentService commentS, IPostService postS)
    {
        _commentS = commentS;
        _postS = postS;
    }

    [HttpPost]
    [Route("/Post")]
    public async Task<IActionResult> PostAsync(CommentModel comment)
    {
        var entity = comment.ToEntity();

        var post = await _postS.GetAsync(entity.PostId);

        post.Comments.Append(entity);
        await _postS.UpdateAsync(post);


        var result = await _commentS.InsertAsync(entity);

        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute] Guid id, CommentModel comment)
    {
        if (!await _commentS.ExistsAsync(id))
        {
            return NotFound();
        }
        var entity = comment.ToEntity();
        entity.Id = id;
        var result = await _commentS.UpdateAsync(entity);

        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = await _commentS.DeleteAsync(id);
        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest();
    }
}