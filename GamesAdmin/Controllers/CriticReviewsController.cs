using GamesAdmin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GamesAdmin.Controllers
{
    [Authorize(Roles = "critic")]
    public class CriticReviewsController : Controller
    {
        private readonly GamesAdminContext _context;
        private string? _userId;

        //Is called after constructir code but before action method.
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            _userId = User.GetUserId();
        }

        public CriticReviewsController(GamesAdminContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _context.CriticReview
                .Where(review => review.CreatedBy == _userId)
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

        public async Task<IActionResult> Create(int gameId)
        {
            //Check if a review exists with the same game and author
            var currentReview = await _context.CriticReview.FirstOrDefaultAsync(r =>
            r.GameId == gameId && r.CreatedBy == _userId);

            //Send elsewhere depending on state
            if (currentReview != null)
            {
                if (!currentReview.isPublished)
                {
                    return RedirectToAction(nameof(Edit), new { id = currentReview.Id });
                }

                return RedirectToAction(nameof(Details), new { id = currentReview.Id });
            }

            var game = await _context.Game.FindAsync(gameId);

            if (game == null)
                return NotFound();

            ViewBag.GameId = gameId;
            ViewBag.GameTitle = game.Title;
            ViewBag.GameImageUrl = game.ImageUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameId,Description,Rating,isPublished")] CriticReview review)
        {
            review.CreatedDate = DateTime.Now;
            review.CreatedBy = _userId!;

            if (ModelState.IsValid)
            {
                //Check if a review exists with the same game and author
                bool isExisting = await _context.CriticReview.AnyAsync(r =>
                    r.GameId == review.GameId &&
                    r.CreatedBy == _userId);

                //
                if (isExisting)
                {
                    ModelState.AddModelError("", "You have already reviewed this game.");
                }
                else { 
                    _context.Add(review);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new {id = review.Id});
                }
            }

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
            } else if (review.isPublished || review.CreatedBy != _userId)
            {
                return StatusCode(403);
            }
            var game = await _context.Game.FindAsync(review.GameId);

            ViewBag.GameId = game!.Id;
            ViewBag.GameTitle = game.Title;
            ViewBag.GameImageUrl = game.ImageUrl;

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameId,Description,Rating,isPublished")] CriticReview review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            var editedReview = await _context.CriticReview.FindAsync(id);

            if (editedReview == null )
            {
                return NotFound();
            }
            else if (editedReview.isPublished || editedReview.CreatedBy != _userId)
            {
                return StatusCode(403);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    editedReview.Description = review.Description;
                    editedReview.Rating = review.Rating;
                    editedReview.isPublished = review.isPublished;
                    editedReview.CreatedDate = DateTime.Now;

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

                return RedirectToAction(nameof(Details), new {id = review.Id});
            }

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

            if (review == null)
            {
                return NotFound();
            } else if (review.CreatedBy != _userId || review.isPublished)
            {
                return StatusCode(403);
            }

            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.CriticReview.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }
            else if (review.CreatedBy != _userId || review.isPublished)
            {
                return StatusCode(403);
            } else 
            {
                _context.CriticReview.Remove(review);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Publish(int id)
        {
            var review = await _context.CriticReview
                .FirstOrDefaultAsync(r => r.Id == id && r.CreatedBy == _userId);

            if (review == null)
                return NotFound();

            review.isPublished = true;
            review.CreatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = review.Id });
        }

        private bool CriticReviewExists(int id)
        {
            return _context.CriticReview.Any(review => review.Id == id);
        }
    }
}
