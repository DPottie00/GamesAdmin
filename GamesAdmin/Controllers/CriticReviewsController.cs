using GamesAdmin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GamesAdmin.Controllers
{
    [Authorize(Roles = "administrator")]
    public class CriticReviewsController : Controller
    {
        private readonly GamesAdminContext _context;

        public CriticReviewsController(GamesAdminContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _context.CriticReview
                .Include(review => review.Game)
                .ToListAsync();

            return View(reviews);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.CriticReview
                .Include(review => review.Game)
                .FirstOrDefaultAsync(review => review.Id == id);

            return review == null ? NotFound() : View(review);
        }

        public IActionResult Create()
        {
            PopulateGames();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,Description,Rating,isPublished,CreatedBy,CreatedDate")] CriticReview review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateGames(review.GameId);
            return View(review);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.CriticReview.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            PopulateGames(review.GameId);
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameId,Description,Rating,isPublished,CreatedBy,CreatedDate")] CriticReview review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriticReviewExists(review.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            PopulateGames(review.GameId);
            return View(review);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.CriticReview
                .Include(review => review.Game)
                .FirstOrDefaultAsync(review => review.Id == id);

            return review == null ? NotFound() : View(review);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.CriticReview.FindAsync(id);
            if (review != null)
            {
                _context.CriticReview.Remove(review);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void PopulateGames(int? selectedGameId = null)
        {
            ViewData["GameId"] = new SelectList(_context.Game.OrderBy(game => game.Title), "Id", "Title", selectedGameId);
        }

        private bool CriticReviewExists(int id)
        {
            return _context.CriticReview.Any(review => review.Id == id);
        }
    }
}
