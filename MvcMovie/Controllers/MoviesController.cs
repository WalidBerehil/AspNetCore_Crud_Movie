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
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var mvcMovieContext = _context.Movie.Include(m => m.actor);
            return View(await mvcMovieContext.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.actor)
                .Include(m => m.categories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            List<Category> s = new List<Category>();
            foreach (CategoryMovie cm in movie.categories)
            {
                s.Add(await _context.Category.FirstOrDefaultAsync(m => m.Id == cm.CategoryId));

            }
            ViewData["categories"] = s;
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["ActorId"] = new SelectList(_context.Actor, "Id", "LastName");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Title,ReleaseDate,Price,ActorId,CategoryIdHelper,categories")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                //var a = movie.CategoryIdHelper;
                //var b = movie.categories;
                _context.Add(movie);
                await _context.SaveChangesAsync();
                List<CategoryMovie> CM = new List<CategoryMovie>();
                if (movie.CategoryIdHelper != null)
                {
                    foreach (var x in movie.CategoryIdHelper)
                    {
                        CategoryMovie c = new CategoryMovie() { MovieId = movie.Id, CategoryId = x };
                        CM.Add(c);

                    }
                    movie.categories = CM;
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }



                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorId"] = new SelectList(_context.Actor, "Id", "LastName", movie.ActorId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", movie.categories);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.Include(m => m.actor)
   .Include(m => m.categories).FirstOrDefaultAsync(m => m.Id == id);

            // *********Add CategoryMovie*************
            List<int> li = new List<int>();
            if (movie.categories != null)
            {
                foreach (CategoryMovie cathelp in movie.categories)
                {
                    int c = cathelp.CategoryId;
                    li.Add(c);

                }
                movie.CategoryIdHelper = li;

            }
            // ********************************

            if (movie == null)
            {
                return NotFound();
            }
            ViewData["ActorId"] = new SelectList(_context.Actor, "Id", "LastName", movie.ActorId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", movie.categories);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Title,ReleaseDate,Price,ActorId,CategoryIdHelper,categories")] Movie movie)
        {

            if (id != movie.Id)
            {
                return NotFound();
            }

            // **********Delete CategoryMovie by MovieId**********
            IQueryable<CategoryMovie> CategoryMoviesQuery =
            from n in _context.CategoryMovie
            where n.MovieId == id
            select n;
            _context.CategoryMovie.RemoveRange(CategoryMoviesQuery);

            await _context.SaveChangesAsync();
            // *********Add CategoryMovie*************
            List<CategoryMovie> CM = new List<CategoryMovie>();
            if (movie.CategoryIdHelper != null)
            {
                foreach (var cathelp in movie.CategoryIdHelper)
                {
                    CategoryMovie c = new CategoryMovie() { MovieId = movie.Id, CategoryId = cathelp };
                    CM.Add(c);

                }
                movie.categories = CM;
                _context.CategoryMovie.AddRange(movie.categories);
                await _context.SaveChangesAsync();
            }
            // ********************************

            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));


            ViewData["ActorId"] = new SelectList(_context.Actor, "Id", "LastName", movie.ActorId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", movie.categories);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.actor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
