using ReleaseManager.Api.Commons;
using ReleaseManager.Model.Models;

namespace ReleaseManager.Api.Repositories;

public interface IReleaseRepository
{
    Task<List<Release>> GetAllAsync();

    Task<Result<Release>> FindAsync(int id);

    bool Exists(int id);

    Task<Release> Add(Release release);

    Task<Result> Update(int id, Release release);

    Task<Result> Delete(int id);
}