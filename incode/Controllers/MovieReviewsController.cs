using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using incode.Models;

namespace incode.Controllers
{
    public class MovieReviewsController : Controller
    {
        private readonly incodedatabaseContext _context;

        public MovieReviewsController(incodedatabaseContext context)
        {
            _context = context;
        }

        // GET: MovieReviews
        public async Task<IActionResult> Index()
        {
            var incodedatabaseContext = _context.MovieReviews.Include(m => m.LinkedPost).Include(m => m.Movie).Include(m => m.User);
            return View(await incodedatabaseContext.ToListAsync());
        }

        // GET: MovieReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieReview = await _context.MovieReviews
                .Include(m => m.LinkedPost)
                .Include(m => m.Movie)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (movieReview == null)
            {
                return NotFound();
            }

            return View(movieReview);
        }

        // GET: MovieReviews/Create
        public IActionResult Create()
        {
            ViewData["LinkedPostId"] = new SelectList(_context.Posts, "PostsId", "PostsContent");
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname");
            return View();
        }

        // POST: MovieReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,MovieId,UserId,OverallRating,StoryRating,ActingRating,VisualRating,MusicRating,Title,Content,IsSpoiler,HelpfulCount,ViewCount,LinkedPostId,CreatedAt,UpdatedAt")] MovieReview movieReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LinkedPostId"] = new SelectList(_context.Posts, "PostsId", "PostsContent", movieReview.LinkedPostId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title", movieReview.MovieId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", movieReview.UserId);
            return View(movieReview);
        }

        // GET: MovieReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieReview = await _context.MovieReviews.FindAsync(id);
            if (movieReview == null)
            {
                return NotFound();
            }
            ViewData["LinkedPostId"] = new SelectList(_context.Posts, "PostsId", "PostsContent", movieReview.LinkedPostId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title", movieReview.MovieId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", movieReview.UserId);
            return View(movieReview);
        }

        // POST: MovieReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewId,MovieId,UserId,OverallRating,StoryRating,ActingRating,VisualRating,MusicRating,Title,Content,IsSpoiler,HelpfulCount,ViewCount,LinkedPostId,CreatedAt,UpdatedAt")] MovieReview movieReview)
        {
            if (id != movieReview.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieReviewExists(movieReview.ReviewId))
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
            ViewData["LinkedPostId"] = new SelectList(_context.Posts, "PostsId", "PostsContent", movieReview.LinkedPostId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title", movieReview.MovieId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", movieReview.UserId);
            return View(movieReview);
        }

        // GET: MovieReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieReview = await _context.MovieReviews
                .Include(m => m.LinkedPost)
                .Include(m => m.Movie)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (movieReview == null)
            {
                return NotFound();
            }

            return View(movieReview);
        }

        // POST: MovieReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieReview = await _context.MovieReviews.FindAsync(id);
            if (movieReview != null)
            {
                _context.MovieReviews.Remove(movieReview);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieReviewExists(int id)
        {
            return _context.MovieReviews.Any(e => e.ReviewId == id);
        }
    }
}
