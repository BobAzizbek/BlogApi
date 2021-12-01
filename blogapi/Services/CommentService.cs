namespace blogapi.Services
{
    public class CommentService : ICommentService
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<CommentService> _logger;
        private readonly IPostService _postS;

        public CommentService(BlogDbContext context, ILogger<CommentService> logger, IPostService postS)
        {
            _context = context;
            _logger = logger;
            _postS = postS;
        }
        public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id)
        {
            var comment = await GetAsync(id);
            if (comment is default(Comment))
            {
                return (false, new Exception("Not found."));
            }
            try
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Comment deleted in DB. ID: {comment.Id}");
                return (true, null);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Deleting comment in DB failed.\nError:{e.Message}", e);
                return (false, e);
            }
        }

        public Task<bool> ExistsAsync(Guid id)
            => _context.Comments.AnyAsync(c => c.Id == id);

        public Task<List<Comment>> GetAllAsync()
            => _context.Comments.ToListAsync();

        public Task<Comment> GetAsync(Guid id)
            => _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<(bool IsSuccess, Exception Exception)> InsertAsync(Comment comment)
        {
            try
            {
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Comment created in DB. ID: {comment.Id}");
                return (true, null);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Creating comment in DB failed.\nError:{e.Message}", e);
                return (false, e);
            }
        }

        public async Task<(bool IsSuccess, Exception Exception)> UpdateAsync(Comment comment)
        {
            if (!await _postS.ExistsAsync(comment.PostId))
            {
                return (false, new Exception("Not found"));
            }
            try
            {
                _context.Comments.Update(comment);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Comment updated in DB. ID: {comment.Id}");
                return (true, null);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Updating comment in DB failed.\nError:{e.Message}", e);
                return (false, e);
            }
        }
    }
}