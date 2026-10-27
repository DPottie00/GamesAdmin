
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamesAdmin.Models;
using GamesAdmin.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

public class GamesController : Controller
{
    private readonly GamesAdminContext _context;
    private readonly AzureStorageService _blobService;

    public GamesController(GamesAdminContext context, AzureStorageService azureBlobService)
    {
        _context = context;
        _blobService = azureBlobService;
    }

    // GET: GAMES
    public async Task<IActionResult> Index(string sortOrder)    
    {
        // Take the games list
        var games = _context.Game.AsQueryable();

        // Switch case to execute sortOrder, will take the game list and order by the sortOrder
        games = sortOrder switch
        {
            "title" => games.OrderBy(game => game.Title),
            "releaseDate" => games.OrderBy(game => game.ReleaseDate),

            // Fallback
            _ => games.OrderBy(game => game.ReleaseDate)
        };

        return View(await games.ToListAsync());
    }

    // GET: GAMES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Game
            .Include(m => m.GenreId)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (game == null)
        {
            return NotFound();
        }

        return View(game);
    }

    // GET: GAMES/Create
    public IActionResult Create()
    {
        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var audienceRating = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audienceRating, "Id", "Title");

        return View();
    }

    // POST: GAMES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,GenreId,AudienceRatingId,Title,Summary,TimeToBeat,ReleaseDate,Developer,Platform,ImageUrl")] Game game, IFormFile imageFile)
    {
        if (ModelState.IsValid)
        {


            string imageUrl = await _blobService.UploadFileAsync(imageFile, imageFile.FileName);

            game.ImageUrl = imageUrl;

            _context.Add(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var audienceRating = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audienceRating, "Id", "Title");

        return View(game);
    }

    // GET: GAMES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Game.FindAsync(id);
        if (game == null)
        {
            return NotFound();
        }

        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var audienceRating = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audienceRating, "Id", "Title");

        return View(game);
    }

    // POST: GAMES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Summary,Genre,Rating,TimeToBeat,ReleaseDate,Developer,Platform")] Game game)
    {
        if (id != game.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(game);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(game.Id))
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

        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var audienceRating = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audienceRating, "Id", "Title");

        return View(game);
    }

    // GET: GAMES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Game
            .FirstOrDefaultAsync(m => m.Id == id);
        if (game == null)
        {
            return NotFound();
        }

        return View(game);
    }

    // POST: GAMES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var game = await _context.Game.FindAsync(id);
        if (game != null)
        {
            _context.Game.Remove(game);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GameExists(int? id)
    {
        return _context.Game.Any(e => e.Id == id);
    }
}
