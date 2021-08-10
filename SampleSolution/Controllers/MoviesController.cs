using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data;
using SampleSolution.Models;
using SampleSolution.Repository.Movie;
using SampleSolution.ViewModels.Movies;

namespace SampleSolution.Controllers
{
    public class MoviesController : Controller
    {
        //private readonly SampleSolutionContext _context;
        private readonly IMovieRepository movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            //_context = context;
            this.movieRepository = movieRepository;
        }



        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            var genre = movieRepository.GetGenre();

            var movies = movieRepository.GetMovie();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genre.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return View(movieGenreVM);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var movie = await _context.Movie
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var movie = await movieRepository.Details(id);
            if (movie == null)
            {
                return NotFound();
            }

            var movieviewmodel = new MovieViewModel(movie);
                      

            return View(movieviewmodel);
            //return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel postmovie)
        {
            var movie = new Movie
            {
                Id = postmovie.Id,
                Rating = postmovie.Rating,
                Genre = postmovie.Genre,
                Price = postmovie.Price,
                ReleaseDate = postmovie.ReleaseDate,
                Title = postmovie.Title
            };

            if (ModelState.IsValid)
            {
                await movieRepository.Add(movie);                
                return RedirectToAction(nameof(Index));
            }
            return View(postmovie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var movie = await _context.Movie.FindAsync(id);
            var movie = await movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }

            var movieviewmodel = new MovieViewModel(movie);

            return View(movieviewmodel);
            //return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieViewModel postmovie)
        {
            if (id != postmovie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var movie = await movieRepository.GetMovieById(id);
                    movie.Price = postmovie.Price;
                    movie.ReleaseDate = postmovie.ReleaseDate;
                    movie.Title = postmovie.Title;
                    movie.Rating = postmovie.Rating;
                    
                    await movieRepository.Update(movie);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(postmovie.Id))
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
            return View(postmovie);
        }
        
        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await movieRepository.GetMovieById(id);
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
            await movieRepository.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return movieRepository.MovieExists(id);
        }
    }
}
