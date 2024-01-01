using APIProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APIProject.Controllers
{

    public class UserProfileController : Controller
    {
        private readonly APIDBContext _context;

        public UserProfileController(APIDBContext context)
        {
            _context = context;
        }
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetuserProfile()
        {
            try
            {
                var userProfiles = await _context.UserProfiles.ToListAsync();
                return Ok(userProfiles);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Route("api/[controller]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserProfileById(int id)
        {
            try
            {
                var userProfile = await _context.UserProfiles.FindAsync(id);

                if (userProfile == null)
                {
                    return NotFound();
                }

                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: /UserProfile
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null)
            {
                var userProfiles = await _context.UserProfiles.ToListAsync();
                return View(userProfiles);
            }
            else
            {
                return RedirectToAction("Login", "Home"); // Redirect to the login action if session variables are not set
            }
        }


        // GET: /UserProfile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(m => m.ProfileId == id);

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // GET: /UserProfile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /UserProfile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileId,UserId")] UserProfile userProfile)
        {
            try
            {
                _context.Add(userProfile);
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

        // GET: /UserProfile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return View(userProfile);
        }

        // POST: /UserProfile/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfileId,UserId")] UserProfile userProfile)
        {
            if (id != userProfile.ProfileId)
            {
                return NotFound();
            }
            try
            {
                try
                {
                    _context.Update(userProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(userProfile.ProfileId))
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

        // GET: /UserProfile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(m => m.ProfileId == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // POST: /UserProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userProfile = await _context.UserProfiles.FindAsync(id);
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfiles.Any(e => e.ProfileId == id);
        }
    }
}
