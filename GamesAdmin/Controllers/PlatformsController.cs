
using GamesAdmin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "administrator")]
public class PlatformsController : Controller
{
    private readonly GamesAdminContext _context;

    public PlatformsController(GamesAdminContext context)
    {
        _context = context;
    }

    // GET: PLATFORMS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Platform.ToListAsync());
    }

    // GET: PLATFORMS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var platform = await _context.Platform
            .FirstOrDefaultAsync(m => m.Id == id);
        if (platform == null)
        {
            return NotFound();
        }

        return View(platform);
    }

    // GET: PLATFORMS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PLATFORMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Platform platform)
    {
        if (ModelState.IsValid)
        {
            _context.Add(platform);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(platform);
    }

    // GET: PLATFORMS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var platform = await _context.Platform.FindAsync(id);
        if (platform == null)
        {
            return NotFound();
        }
        return View(platform);
    }

    // POST: PLATFORMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name")] Platform platform)
    {
        if (id != platform.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(platform);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformExists(platform.Id))
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
        return View(platform);
    }

    // GET: PLATFORMS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var platform = await _context.Platform
            .FirstOrDefaultAsync(m => m.Id == id);
        if (platform == null)
        {
            return NotFound();
        }

        return View(platform);
    }

    // POST: PLATFORMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var platform = await _context.Platform.FindAsync(id);
        if (platform != null)
        {
            _context.Platform.Remove(platform);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PlatformExists(int? id)
    {
        return _context.Platform.Any(e => e.Id == id);
    }
}
