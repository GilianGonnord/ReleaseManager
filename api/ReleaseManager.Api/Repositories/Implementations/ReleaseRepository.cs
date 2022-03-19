using Microsoft.EntityFrameworkCore;
using ReleaseManager.Api.Commons;
using ReleaseManager.Model;
using ReleaseManager.Model.Models;

namespace ReleaseManager.Api.Repositories;

public class ReleaseRepository : IReleaseRepository
{
    private readonly ReleaseManagerContext _context;

    public ReleaseRepository(ReleaseManagerContext context)
    {
        _context = context;
    }

    public async Task<Release> Add(Release release)
    {
        _context.Releases.Add(release);

        await _context.SaveChangesAsync();

        return release;
    }

    public async Task<Result> Delete(int id)
    {
        var release = await _context.Releases.FindAsync(id);
        if (release == null)
            return Result.Fail(CommonErrors.NotFound);

        _context.Releases.Remove(release);
        await _context.SaveChangesAsync();

        return Result.Ok();
    }

    public bool Exists(int id)
    {
        return _context.Releases.Any(e => e.Id == id);
    }

    public async Task<Result<Release>> FindAsync(int id)
    {
        var release = await _context.Releases.FindAsync(id);

        if (release == null)
            return Result<Release>.Fail(CommonErrors.NotFound);

        return Result<Release>.Ok(release);
    }

    public Task<List<Release>> GetAllAsync()
    {
        return _context.Releases.ToListAsync();
    }

    public async Task<Result> Update(int id, Release release)
    {
        _context.Entry(release).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReleaseExists(id))
                return Result.Fail(CommonErrors.NotFound);

            throw;
        }

        return Result.Ok();
    }

    private bool ReleaseExists(int id)
    {
        return _context.Releases.Any(e => e.Id == id);
    }
}