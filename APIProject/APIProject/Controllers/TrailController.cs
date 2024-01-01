using APIProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIProject.Controllers
{
    public class TrailController : Controller
    {
        private readonly APIDBContext _context;

        public TrailController(APIDBContext context)
        {
            _context = context;
        }
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trail>>> GetTrails()
        {
            var trails = await _context.Trails.ToListAsync();
            return trails;
        }

        // GET: api/User/{id}
        [Route("api/[controller]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Trail>> GetTrailById(int id)
        {
            var trail = await _context.Trails.FindAsync(id);

            if (trail == null)
            {
                return NotFound();
            }

            return trail;
        }



        public async Task<IActionResult> Index()
        {

            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null &&
                 HttpContext.Session.GetString("RoleSession") == "Admin")
            {
                var trails = await _context.Trails.ToListAsync();
                return View(trails);
            }
            else
            {
                return RedirectToAction("Login", "Home"); // Redirect to the login action if session variables are not set
            }
        }


        // GET: /Trail/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Trail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrailId,Name,Description,Location,Difficulty,Length,ElevationGain,ElevationLoss,Features")] Trail trail)
        {
            try
            {
                _context.Add(trail);
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

        // GET: /Trail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trail = await _context.Trails.FindAsync(id);
            if (trail == null)
            {
                return NotFound();
            }
            return View(trail);
        }

        // POST: /Trail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrailId,Name,Description,Location,Difficulty,Length,ElevationGain,ElevationLoss,Features")] Trail trail)
        {
            if (id != trail.TrailId)
            {
                return NotFound();
            }

            try
            {
                try
                {
                    _context.Update(trail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrailExists(trail.TrailId))
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

        // GET: /Trail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trail = await _context.Trails.FirstOrDefaultAsync(m => m.TrailId == id);
            if (trail == null)
            {
                return NotFound();
            }

            return View(trail);
        }

        // POST: /Trail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trail = await _context.Trails.FindAsync(id);
            _context.Trails.Remove(trail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trail = await _context.Trails.FirstOrDefaultAsync(m => m.TrailId == id);

            if (trail == null)
            {
                return NotFound();
            }

            return View(trail);
        }
        private bool TrailExists(int id)
        {
            return _context.Trails.Any(e => e.TrailId == id);
        }
    }
}
