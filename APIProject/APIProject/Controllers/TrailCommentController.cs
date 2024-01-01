using APIProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APIProject.Controllers
{
    public class TrailCommentController : Controller
    {
        private readonly APIDBContext _context;

        public TrailCommentController(APIDBContext context)
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
        // GET: /Profile
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null)
            {
                var trailComments = await _context.TrailComments.ToListAsync();
                return View(trailComments);
            }
            else
            {
                return RedirectToAction("Login", "Home"); // Redirect to the login action if session variables are not set
            }
        }

        // GET: /TrailComment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trailComment = await _context.TrailComments.FirstOrDefaultAsync(m => m.TrailCommentId == id);

            if (trailComment == null)
            {
                return NotFound();
            }

            return View(trailComment);
        }

        // GET: /TrailComment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /TrailComment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrailCommentId,TrailId,UserId,Comment,DateCreated")] TrailComment trailComment)
        {
            try
            {
                _context.Add(trailComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log or print the exception message for debugging purposes
                Console.WriteLine(ex.Message);
                throw; // Rethrow the exception or handle it accordingly
            }

        }

        // GET: /TrailComment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trailComment = await _context.TrailComments.FindAsync(id);
            if (trailComment == null)
            {
                return NotFound();
            }
            return View(trailComment);
        }

        // POST: /TrailComment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrailCommentId,TrailId,UserId,Comment,DateCreated")] TrailComment trailComment)
        {
            if (id != trailComment.TrailCommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trailComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrailCommentExists(trailComment.TrailCommentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trailComment);
        }

        // GET: /TrailComment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trailComment = await _context.TrailComments.FirstOrDefaultAsync(m => m.TrailCommentId == id);
            if (trailComment == null)
            {
                return NotFound();
            }

            return View(trailComment);
        }

        // POST: /TrailComment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trailComment = await _context.TrailComments.FindAsync(id);
            _context.TrailComments.Remove(trailComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrailCommentExists(int id)
        {
            return _context.TrailComments.Any(e => e.TrailCommentId == id);
        }
    }
}
