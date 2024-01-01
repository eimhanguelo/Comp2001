using APIProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace APIProject.Controllers
{
    public class TrailCommentsViewController : ControllerBase
    {
        private readonly APIDBContext _context;

        public TrailCommentsViewController(APIDBContext context)
        {
            _context = context;
        }

        // GET: api/TrailCommentsView
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrailCommentsView>>> GetTrailCommentsView()
        {
            var trailComments = await _context.TrailCommentsViews.ToListAsync();
            return trailComments;
        }

        // GET: api/TrailCommentsView/{id}
        [Route("api/[controller]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TrailCommentsView>> GetTrailCommentById(int id)
        {
            var trailComment = await _context.TrailCommentsViews.FindAsync(id);

            if (trailComment == null)
            {
                return NotFound();
            }

            return trailComment;
        }

        // POST: api/TrailCommentsView
        [HttpPost]
        public async Task<ActionResult<TrailCommentsView>> PostTrailComment(TrailCommentsView trailComment)
        {
            _context.TrailCommentsViews.Add(trailComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTrailCommentById), new { id = trailComment.TrailCommentId }, trailComment);
        }

        // PUT: api/TrailCommentsView/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrailComment(int id, TrailCommentsView trailComment)
        {
            if (id != trailComment.TrailCommentId)
            {
                return BadRequest();
            }

            _context.Entry(trailComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrailCommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/TrailCommentsView/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrailComment(int id)
        {
            var trailComment = await _context.TrailCommentsViews.FindAsync(id);
            if (trailComment == null)
            {
                return NotFound();
            }

            _context.TrailCommentsViews.Remove(trailComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/TrailCommentsView
        [HttpGet("Index")]
        public async Task<ActionResult<IEnumerable<TrailCommentsView>>> Index()
        {
            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null)
            {
                var trailCommentsView = await _context.TrailCommentsViews.ToListAsync();
                return trailCommentsView;
            }
            else
            {
                // Return an unauthorized response or handle it based on your application's logic
                return Unauthorized();
            }
        }

        private bool TrailCommentExists(int id)
        {
            return _context.TrailCommentsViews.Any(e => e.TrailCommentId == id);
        }
    }
}
