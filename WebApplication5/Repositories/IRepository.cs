using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task Create(T obj);
        Task<Movie> Update(int id,T obj);
        Task<Movie> Delete(int id);

    }

    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> SearchByGenre(string genreName);
    }
}
