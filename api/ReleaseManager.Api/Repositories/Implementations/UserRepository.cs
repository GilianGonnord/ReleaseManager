using Microsoft.EntityFrameworkCore;
using ReleaseManager.Model;
using ReleaseManager.Model.Models;

namespace ReleaseManager.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ReleaseManagerContext _context;

    public UserRepository(ReleaseManagerContext context)
    {
        _context = context;
    }

    public Task<List<User>> GetAllAsync()
    {
        return _context.Users.OrderBy(u => (int)u.Role).ToListAsync();
    }
}