using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchitAPIs.Models;
using WatchitAPIs.Repository_Contract;
using WatchitAPIs.DTOs;
using WatchitAPIs.Services_Contract;
using System.Runtime.CompilerServices;

namespace WatchitAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var Movies = movieService.GetMoviesWithCastAndDirector();
            if (Movies != null)
                return Ok(Movies);
            return BadRequest();
        }

        [HttpGet("{id}",Name ="GetMovieByIdRoute")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await movieService.GetMovieWithCastAndDirectorAsync(id);
            if (movie != null)
                return Ok(movie);
            return BadRequest();
        }

        [HttpGet("TopWatched")]
        public async Task<IActionResult> GetTopWatchedMovies()
        {
            List<Movie> TopMovies = await movieService.GetTopWatchedMoviesAsync();
            if(TopMovies != null)
                return Ok(TopMovies);
            else
                return BadRequest();     
        }

        [HttpPost("AddMovie")]
        [Authorize("Admin")]
        public async Task<IActionResult> AddMovie(Movie movie)
        {
            if(ModelState.IsValid)
            {
                string? url = Url.Link("GetMovieByIdRoute", new { id = movie.Id });
                await movieService.AddAsync(movie);
                return Created(url, movie);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Favorites")]
        public async Task<IActionResult> GetFavoriteMovies(string id)
        {
            var movies = await movieService.GetUsersFavoriteMovies(id);
            if(movies != null)
                return Ok(movies);
            else
                return BadRequest("Your Favorite List Is Empty");
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
           bool result = await movieService.DeleteAsync(id);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> PutMovie(int id,Movie newMovie)
        {
            var result = await movieService.UpdateEntityAsync(id, newMovie);
            if(result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("RecetMovies")]
        public async Task<IActionResult> GetRecentMovies()
        {
            var movies = await movieService.GetRecentMovies();
            if (movies != null)
                return Ok(movies);
            else
                return BadRequest();
        }

        [HttpGet("SearchName")]
        public IActionResult GetMoviesBySearch(string Name)
        {

            var movies = movieService.GetByName(Name);
            if (movies != null)
                return Ok(movies);
            else
                return BadRequest();
        }
        [HttpGet("SearchGenres")]
        public async Task<IActionResult> GetMoviesByGenres(List<string> genres)
        {
            var movies = movieService.GetMoviesByGenres(genres);
            if(movies != null)
                return Ok(movies);
            return BadRequest();
        }

        [HttpGet("TopRated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = movieService.GetTopRatedMovies();
            if(movies != null)
                return Ok(movies);
            return BadRequest();
        }
    }
}
