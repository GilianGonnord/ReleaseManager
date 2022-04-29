using ReleaseManager.Model.Models;

namespace ReleaseManager.Api.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
}