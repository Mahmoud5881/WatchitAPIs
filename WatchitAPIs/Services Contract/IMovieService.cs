using WatchitAPIs.DTOs;
using WatchitAPIs.Models;
using WatchitAPIs.Repository_Contract;

namespace WatchitAPIs.Services_Contract;

public interface IMovieService : IGenericRepository<Movie>
{
    Task<List<Movie>> GetTopWatchedMoviesAsync();

    Task<List<Movie>> GetUsersFavoriteMovies(string id);

    Task<List<Movie>> GetRecentMovies();

    List<Movie> GetByName(string title);
    
    List<Movie> GetMoviesByGenres(List<string> genres);

    List<MovieWithCastAndDirectorDTO> GetMoviesWithCastAndDirector();

    Task<MovieWithCastAndDirectorDTO> GetMovieWithCastAndDirectorAsync(int id);

    Task<List<Movie>> GetTopRatedMovies();
}