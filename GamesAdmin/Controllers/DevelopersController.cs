
using GamesAdmin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "administrator")]
public class DevelopersController : Controller
{

    private readonly GamesAdminContext _context;

    public DevelopersController(GamesAdminContext context)
    {
        _context = context;
    }

    // GET: DEVELOPERS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Developer.ToListAsync());
    }

    // GET: DEVELOPERS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var developer = await _context.Developer
            .FirstOrDefaultAsync(m => m.Id == id);
        if (developer == null)
        {
            return NotFound();
        }

        return View(developer);
    }

    // GET: DEVELOPERS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DEVELOPERS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Developer developer)
    {
        if (ModelState.IsValid)
        {
            _context.Add(developer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(developer);
    }

    // GET: DEVELOPERS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var developer = await _context.Developer.FindAsync(id);
        if (developer == null)
        {
            return NotFound();
        }
        return View(developer);
    }

    // POST: DEVELOPERS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name")] Developer developer)
    {
        if (id != developer.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(developer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperExists(developer.Id))
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
        return View(developer);
    }

    // GET: DEVELOPERS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var developer = await _context.Developer
            .FirstOrDefaultAsync(m => m.Id == id);
        if (developer == null)
        {
            return NotFound();
        }

        return View(developer);
    }

    // POST: DEVELOPERS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var developer = await _context.Developer.FindAsync(id);
        if (developer != null)
        {
            _context.Developer.Remove(developer);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DeveloperExists(int? id)
    {
        return _context.Developer.Any(e => e.Id == id);
    }
}
