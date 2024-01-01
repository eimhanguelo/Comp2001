using APIProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Controllers
{
    public class UserController : Controller
    {
        private readonly APIDBContext _context;

        public UserController(APIDBContext context)
        {
            this._context = context;
        }
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        [Route("api/[controller]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        // GET: /User
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null &&
                 HttpContext.Session.GetString("RoleSession") == "Admin")
            {
                var users = await _context.Users.ToListAsync();
                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Home"); // Redirect to the login action if session variables are not set
            }
        }

        // GET: /User/Create
        public IActionResult Create()
        {
            List<SelectListItem> Role = new()
            {
                new SelectListItem {Value="Regular User" , Text="Regular_User"},
                new SelectListItem {Value="Admin" , Text="Admin"},
            };
            ViewBag.Role = Role;
            return View();
        }

        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Email,Password,Role")] User user)
        {
            try
            {
                _context.Add(user);
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

        // GET: /User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,Email,Password,Role")] User user)
        {

            if (id != user.UserId)
            {
                return NotFound();
            }

            try
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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

        // GET: /User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
