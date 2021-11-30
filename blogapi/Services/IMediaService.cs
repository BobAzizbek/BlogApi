namespace blogapi.Services;
public interface IMediaService
{
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsAsync(IEnumerable<Guid> id);
    Task<Media> GetAsync(Guid id);
    Task<List<Media>> GetAllAsync();
    Task<List<Media>> GetAllAsync(IEnumerable<Guid> id);
    Task<(bool IsSuccess, Exception Exception)> InsertAsync(List<Media> media);
    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id);
}