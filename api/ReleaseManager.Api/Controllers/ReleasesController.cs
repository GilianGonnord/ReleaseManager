#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReleaseManager.Model;
using ReleaseManager.Model.Models;

namespace ReleaseManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReleasesController : ControllerBase
    {
        private readonly ReleaseManagerContext _context;

        public ReleasesController(ReleaseManagerContext context)
        {
            _context = context;
        }

        // GET: api/Releases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Release>>> GetReleases()
        {
            return await _context.Releases.ToListAsync();
        }

        // GET: api/Releases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Release>> GetRelease(int id)
        {
            var release = await _context.Releases.FindAsync(id);

            if (release == null)
                return NotFound();

            return release;
        }

        // PUT: api/Releases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelease(int id, Release release)
        {
            if (id != release.Id)
                return BadRequest();

            _context.Entry(release).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReleaseExists(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // POST: api/Releases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Release>> PostRelease(Release release)
        {
            _context.Releases.Add(release);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelease", new { id = release.Id }, release);
        }

        // DELETE: api/Releases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRelease(int id)
        {
            var release = await _context.Releases.FindAsync(id);
            if (release == null)
                return NotFound();

            _context.Releases.Remove(release);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReleaseExists(int id)
        {
            return _context.Releases.Any(e => e.Id == id);
        }
    }
}
