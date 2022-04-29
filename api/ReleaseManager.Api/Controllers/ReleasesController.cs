#nullable disable

using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReleaseManager.Api.Repositories;
using ReleaseManager.Api.ViewModels;
using ReleaseManager.Model.Enums;

namespace ReleaseManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Policy = "User")]
public class ReleasesController : ControllerBase
{
    private readonly IReleaseRepository _releaseRepostitory;

    public ReleasesController(IReleaseRepository releaseRepostitory)
    {
        _releaseRepostitory = releaseRepostitory;
    }

    // GET: api/Releases
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReleaseViewModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReleaseViewModel>>> GetReleases()
    {
        var releases = await _releaseRepostitory.GetAllAsync();

        var releaseVms = releases.Select(ReleaseViewModel.FromModel);

        return Ok(releaseVms);
    }

    // GET: api/Releases/5
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ReleaseViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReleaseViewModel>> GetRelease(int id)
    {
        var release = await _releaseRepostitory.FindAsync(id);

        if (release.Failure)
            return NotFound();

        var releaseVm = ReleaseViewModel.FromModel(release.Item);

        return Ok(releaseVm);
    }

    // PUT: api/Releases/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRelease(int id, ReleaseViewModel releaseVm)
    {
        if (id != releaseVm.Id)
            return BadRequest();

        var release = releaseVm.ToModel();

        var result = await _releaseRepostitory.Update(id, release);

        if (result.IsError(CommonErrors.NotFound))
            return NotFound();

        return NoContent();
    }

    // POST: api/Releases
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ReleaseViewModel>> PostRelease(ReleaseViewModel releaseVm)
    {
        var release = await _releaseRepostitory.Add(releaseVm.ToModel());

        return CreatedAtAction("GetRelease", new { id = release.Id }, ReleaseViewModel.FromModel(release));
    }

    // DELETE: api/Releases/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRelease(int id)
    {
        var result = await _releaseRepostitory.Delete(id);

        if (result.IsError(CommonErrors.NotFound))
            return NotFound();

        return NoContent();
    }
}