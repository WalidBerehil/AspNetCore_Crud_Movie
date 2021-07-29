using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class CategoryMoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public CategoryMoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: CategoryMovies
        public async Task<IActionResult> Index()
        {
            var mvcMovieContext = _context.CategoryMovie.Include(c => c.category).Include(c => c.movie);
            return View(await mvcMovieContext.ToListAsync());
        }

        // GET: CategoryMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryMovie = await _context.CategoryMovie
                .Include(c => c.category)
                .Include(c => c.movie)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (categoryMovie == null)
            {
                return NotFound();
            }

            return View(categoryMovie);
        }

        // GET: CategoryMovies/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id");
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id");
            return View();
        }

        // POST: CategoryMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,MovieId")] CategoryMovie categoryMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", categoryMovie.CategoryId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", categoryMovie.MovieId);
            return View(categoryMovie);
        }

        // GET: CategoryMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryMovie = await _context.CategoryMovie.FindAsync(id);
            if (categoryMovie == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", categoryMovie.CategoryId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", categoryMovie.MovieId);
            return View(categoryMovie);
        }

        // POST: CategoryMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,MovieId")] CategoryMovie categoryMovie)
        {
            if (id != categoryMovie.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryMovieExists(categoryMovie.CategoryId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", categoryMovie.CategoryId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", categoryMovie.MovieId);
            return View(categoryMovie);
        }

        // GET: CategoryMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryMovie = await _context.CategoryMovie
                .Include(c => c.category)
                .Include(c => c.movie)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (categoryMovie == null)
            {
                return NotFound();
            }

            return View(categoryMovie);
        }

        // POST: CategoryMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryMovie = await _context.CategoryMovie.FindAsync(id);
            _context.CategoryMovie.Remove(categoryMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryMovieExists(int id)
        {
            return _context.CategoryMovie.Any(e => e.CategoryId == id);
        }
    }
}
