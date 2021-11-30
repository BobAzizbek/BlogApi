namespace blogapi.Services;
public interface IPostService
{
    Task<bool> ExistsAsync(Guid id);
    Task<Post> GetAsync(Guid id);
    Task<List<Post>> GetAllAsync();
    Task<List<Post>> GetJsonAsync();
    Task<(bool IsSuccess, Exception Exception)> InsertAsync(Post post);
    Task<(bool IsSuccess, Exception Exception)> UpdateAsync(Post post);
    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id);
}