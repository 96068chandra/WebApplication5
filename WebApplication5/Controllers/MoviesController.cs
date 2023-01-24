using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication5.Models;
using WebApplication5.Repositories;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IRepository<Movie> _repository;
        public readonly IMovieRepository _movieRepository;
        public readonly IGetRepository<MovieDto> _movieDtoRepository;



        public MoviesController(IRepository<Movie> repository,IMovieRepository movieRepository,IGetRepository<MovieDto> movieDtoRepository)
        {
            _repository = repository;
            _movieRepository= movieRepository;
            _movieDtoRepository = movieDtoRepository;

        }
        [HttpGet]
        [Route("GetAllMovies")]
        public IEnumerable<MovieDto> GetMovies()
        {
            return _movieDtoRepository.GetAll();
        }
        [HttpGet]
        [Route("GetMovieById/{id}",Name ="GetMovieById")]
        public async Task<ActionResult> GetById(int id)
        {
            var movie = await _movieDtoRepository.GetById(id);
            if(movie != null)
            {
                return Ok(movie);
            }
            return NotFound();
        }
        [HttpPost("CreateMovie")]
        public async Task<IActionResult> CreateMovie([FromBody] Movie movie)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(movie);
            return CreatedAtRoute("GetMovieById", new { id = movie.Id },movie);
        }

        [HttpPut("UpdateMovie/{id}")]

        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result=await _repository.Update(id, movie);
            if(result != null)
            {
                return NoContent();
            }
            return NotFound("Movie Not Found");
        }

        [HttpDelete("DeleteMovie/{id}")]

        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await _repository.Delete(id);
            if(result != null )
            {
                return Ok();
            }
            return NotFound("Movie not found");
        }

        [HttpGet("SearchMovie/{genreName}")]
        public async Task<IActionResult> SearchMovieByGenre(string genreName)
        {
            var result = await _movieRepository.SearchByGenre(genreName);
            if(result != null)
            {
                return Ok(result);
            }
            return NotFound("Please provide valid genre");
        }

        [HttpGet("GetGenres")]

        public async Task<IActionResult> GetGenres()
        {
           var genres= await _movieRepository.GetGenres();
            return Ok(genres);
        }


    }
}
