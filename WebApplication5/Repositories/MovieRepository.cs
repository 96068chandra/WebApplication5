using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Repositories
{
    public class MovieRepository : IRepository<Movie>, IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Movie obj)
        {
            if(obj!=null)
            {
                _context.Movies.Add(obj);
                await _context.SaveChangesAsync();
            }
        }

        

        public async Task<Movie> Delete(int id)
        {
            var MovieInDb=await _context.Movies.FindAsync(id);
            if(MovieInDb!=null)
            {
                _context.Movies.Remove(MovieInDb);
                await _context.SaveChangesAsync();
                return MovieInDb;
            }
            return null;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public async Task<Movie> GetById(int id)
        {
            var movie=await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                return movie;
            }
            return null;
        }

        public async Task<Movie> Update(int id, Movie obj)
        {
            var MovieInDb = await _context.Movies.FindAsync(id);
            if (MovieInDb != null)
            {
                MovieInDb.MovieName = obj.MovieName;
                MovieInDb.ProductionName = obj.ProductionName;
                MovieInDb.ReleaseDate = obj.ReleaseDate;
                MovieInDb.GenreId = obj.GenreId;
                _context.Movies.Update(MovieInDb);
                await _context.SaveChangesAsync();
                return MovieInDb;

            }
            return null;
        }
         

        public async Task<IEnumerable<Movie>> SearchByGenre(string genreName)
        {
            if(!string.IsNullOrWhiteSpace(genreName))
            {
                var movies=await _context.Movies.Include(x=>x.Genre).Where(x=>x.Genre.GenreName.Contains(genreName)).ToListAsync();
                return movies;
            }
            return null;
        }
        //IEnumerable IRepository<Movie>.GetAll()
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
