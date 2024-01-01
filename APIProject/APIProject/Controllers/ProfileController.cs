using APIProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APIProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly APIDBContext _context;

        public ProfileController(APIDBContext context)
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
                var profiles = await _context.Profiles.ToListAsync();
                return View(profiles);
            }
            else
            {
                return RedirectToAction("Login", "Home"); // Redirect to the login action if session variables are not set
            }
        }

        // GET: /Profile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles.FirstOrDefaultAsync(m => m.ProfileId == id);

            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // GET: /Profile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Profile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileId,UserId,Name,Description,CreatedAt,UpdatedAt,ProfilePictureUrl")] Profile profile)
        {
            try
            {
                _context.Add(profile);
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
        // GET: /Profile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null &&
                HttpContext.Session.GetString("UserIdSession") == id.ToString())
            {
                var profile = await _context.Profiles.FindAsync(id);
                if (profile == null)
                {
                    return NotFound();
                }

                return View(profile);
            }

            return NotFound(); // Return appropriate response when session check fails
        }


        // POST: /Profile/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfileId,UserId,Name,Description,CreatedAt,UpdatedAt,ProfilePictureUrl")] Profile profile)
        {
            if (id != profile.ProfileId)
            {
                return NotFound();
            }

            try
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.ProfileId))
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
            catch (Exception ex)
            {
                // Log or print the exception message for debugging purposes
                Console.WriteLine(ex.Message);

                throw; // Rethrow the exception or handle it accordingly
            }
        }
        // GET: /Profile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null &&
                HttpContext.Session.GetString("UserIdSession") == id.ToString())
            {
                var profile = await _context.Profiles.FirstOrDefaultAsync(m => m.ProfileId == id);
                if (profile == null)
                {
                    return NotFound();
                }

                return View(profile);
            }

            return NotFound(); // Return appropriate response when session check fails
        }


        // POST: /Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.ProfileId == id);
        }
    }
}
