using Microsoft.EntityFrameworkCore;
using SampleSolution.Data;
using SampleSolution.ViewModels.Movies;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSolution.Repository.Movie
{
    public class MovieRepository : IMovieRepository
    {
        private readonly SampleSolutionContext _context;

        public MovieRepository(SampleSolutionContext context)
        {
            _context = context;
        }

        public async Task<Models.Movie> Add(Models.Movie movie)
        {
            _context.Add(movie);
          var yy=  await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<int> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            return await _context.SaveChangesAsync();
        }

        public async Task<Models.Movie> Details(int? id)
        {
            return await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<string> GetGenre()
        {
            return (from m in _context.Movie
                   orderby m.Genre
                   select m.Genre);
        }

        public IQueryable<Models.Movie> GetMovie()
        {
            return (from m in _context.Movie
                   select m);            
        }

        public async Task<Models.Movie> GetMovieById(int? id)
        {
            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            return movie;
        }

        public bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }

        public async Task<Models.Movie> Update(Models.Movie movie)
        {
            _context.Update(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
    }

    public interface IMovieRepository
    {
        Task<Models.Movie> Details(int? id);
        Task<Models.Movie> GetMovieById(int? id);
        IQueryable<string> GetGenre();
        IQueryable<Models.Movie> GetMovie();
        Task<int> DeleteConfirmed(int id);
        bool MovieExists(int id);
        Task<Models.Movie> Add(Models.Movie movie);
        Task<Models.Movie> Update(Models.Movie movie);
        
    }
}
