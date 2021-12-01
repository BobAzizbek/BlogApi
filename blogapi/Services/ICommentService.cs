namespace blogapi.Services;
public interface ICommentService
{
    Task<bool> ExistsAsync(Guid id);
    Task<Comment> GetAsync(Guid id);
    Task<List<Comment>> GetAllAsync();
    Task<(bool IsSuccess, Exception Exception)> InsertAsync(Comment comment);
    Task<(bool IsSuccess, Exception Exception)> UpdateAsync(Comment comment);
    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id);
}