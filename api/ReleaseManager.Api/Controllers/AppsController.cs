using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReleaseManager.Api.GitPilot;
using ReleaseManager.Api.ViewModels;
using ReleaseManager.Model;
using ReleaseManager.Model.Models;

namespace ReleaseManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppsController : ControllerBase
{
    private readonly ReleaseManagerContext _context;
    private readonly IGitPilot _gitPilot;

    public AppsController(ReleaseManagerContext context, IGitPilot gitPilot)
    {
        _context = context;
        _gitPilot = gitPilot;
    }

    // GET: api/<AppsController>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App>>> Get()
    {
        var apps = await _context.Apps.ToListAsync();

        var appVms = apps.Select(AppViewModel.FromModel);

        return Ok(apps);
    }

    // GET api/<AppsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App>> Get(int id)
    {
        var app = await _context.Apps.FindAsync(id);

        if (app == null)
            return NotFound();

        return Ok(app);
    }

    // GET api/<AppsController>/5/OngoingReleases
    [HttpGet("{id}/OngoingReleases")]
    [ProducesResponseType(typeof(App), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<string>>> GetOngoingReleases(int id)
    {
        var app = await _context.Apps.FindAsync(id);

        if (app == null)
            return NotFound();

        var ongoingReleases = _gitPilot.GetOngoingReleases(_context.Config, app);

        return Ok(ongoingReleases);
    }

    // POST api/<AppsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App>> Post([FromBody] AppViewModel appVm)
    {
        var app = appVm.ToModel();

        _context.Apps.Add(app);

        await _context.SaveChangesAsync();

        await _gitPilot.CloneRepo(_context.Config, app);

        appVm = AppViewModel.FromModel(app);

        return CreatedAtAction("Get", new { id = appVm.Id }, appVm);
    }

    // PUT api/<AppsController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}


    // PUT api/<AppsController>/5/Clone
    [HttpPut("{id}/Clone")]
    public async Task<ActionResult> Clone(int id)
    {
        var app = await _context.Apps.FindAsync(id);

        if (app == null) return NotFound();

        await _gitPilot.CloneRepo(_context.Config, app);

        return NoContent();
    }

    // DELETE api/<AppsController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var app = await _context.Apps.FindAsync(id);

        if (app == null) return NotFound();

        _gitPilot.DeleteRepo(_context.Config, app);

        _context.Apps.Remove(app);

        await _context.SaveChangesAsync();

        return Ok();
    }
}
