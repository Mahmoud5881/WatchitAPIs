using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Generic;
using WatchitAPIs.DTOs;
using WatchitAPIs.EFCore;
using WatchitAPIs.Models;
using WatchitAPIs.Repository;
using WatchitAPIs.Services_Contract;

namespace WatchitAPIs.Services;

public class MovieService : GenericRepository<Movie>, IMovieService
{
    private readonly AppDbContext context;

    public MovieService(AppDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<List<Movie>> GetTopWatchedMoviesAsync() =>
        await context.Set<Movie>().OrderByDescending(m => m.Views).Take(10).ToListAsync();

    public async Task<List<Movie>> GetUsersFavoriteMovies(string id)
    {
        var User = await context.Users
        .Include(u => u.UsersFavorite)
        .ThenInclude(fm => fm.Movie)
        .FirstOrDefaultAsync(u => u.Id == id);

        List<Movie> movies = new List<Movie>();

        foreach (var fav in User.UsersFavorite)
        {
            movies.Add(fav.Movie);
        }

        return movies;
    }

    public async Task<List<Movie>> GetRecentMovies()
    {
        var movies = context.Movies.TakeLast(10);
        return await movies.ToListAsync();
    }

    public List<Movie> GetByName(string title)
    {
        var movies = context.Movies.Where(m=>m.Title == title).ToList();
        return movies;
    }

    public List<Movie> GetMoviesByGenres(List<string> genres)
    {
        var movies = context.Movies.Where(m => genres.All(g => m.Genres.Contains(g))).ToList();
        return movies;
    }

    public List<MovieWithCastAndDirectorDTO> GetMoviesWithCastAndDirector()
    {
        var movies = context.Movies.Include(m => m.Cast).ThenInclude(movieCast => movieCast.Cast)
            .Include(m => m.Director);
            var moviesDto = new List<MovieWithCastAndDirectorDTO>();

        foreach (var movie in movies)
        {
            var movieDto = new MovieWithCastAndDirectorDTO();

            movieDto.Id = movie.Id;
            movieDto.Title = movie.Title;
            movieDto.Duration = movie.Duration;
            movieDto.Genres = movie.Genres;
            movieDto.Language = movie.Language;
            movieDto.ImdbScore = movie.ImdbScore;
            movieDto.Country = movie.Country;
            movieDto.Budget = movie.Budget;
            movieDto.Revenue = movie.Revenue;
            movieDto.Poster = movie.Poster;
            movieDto.Video = movie.Video;
            movieDto.Description = movie.Description;
            movieDto.Views = movie.Views;
            movieDto.Director = new DirectorDTO()
            {
                Id = movie.Director.Id, FirstName = movie.Director.FirstName,
                LastName = movie.Director.LastName
            };
            foreach (var cast in movie.Cast)
            {
                movieDto.Cast.Add(new CastDTO()
                {
                    Id = cast.CastId,
                    FirstName = cast.Cast.FirstName,
                    LastName = cast.Cast.LastName
                });
            }
            moviesDto.Add(movieDto);
        }
        return moviesDto;
    }

    public async Task<MovieWithCastAndDirectorDTO> GetMovieWithCastAndDirectorAsync(int id)
    {
        var movie = await context.Movies.Include(m => m.Cast)
            .ThenInclude(movieCast => movieCast.Cast)
            .Include(m => m.Director)
            .FirstOrDefaultAsync(m => m.Id == id);
        var movieDto = new MovieWithCastAndDirectorDTO();
        movieDto.Id = movie.Id;
        movieDto.Title = movie.Title;
        movieDto.Duration = movie.Duration;
        movieDto.Genres = movie.Genres;
        movieDto.Language = movie.Language;
        movieDto.ImdbScore = movie.ImdbScore;
        movieDto.Country = movie.Country;
        movieDto.Budget = movie.Budget;
        movieDto.Revenue = movie.Revenue;
        movieDto.Poster = movie.Poster;
        movieDto.Video = movie.Video;
        movieDto.Description = movie.Description;
        movieDto.Views = movie.Views;
        movieDto.Director = new DirectorDTO()
        {
            Id = movie.Director.Id, FirstName = movie.Director.FirstName,
            LastName = movie.Director.LastName
        };
        foreach (var cast in movie.Cast)
        {
            movieDto.Cast.Add(new CastDTO()
            {
                Id = cast.CastId,
                FirstName = cast.Cast.FirstName,
                LastName = cast.Cast.LastName
            });
        }
        return movieDto;
    }

    public async Task<List<Movie>> GetTopRatedMovies()
    {
        var moviesRate  = new Dictionary<Movie,int>();
        var movies = context.Movies.ToList();
        foreach (var movie in movies)
        {
            int OverallRate = context.Records
                .Where(m => m.MovieId == movie.Id)
                .Sum(m => m.Rating);

            int Views = context.Records
                .Count(m => m.MovieId == movie.Id);

            moviesRate.Add(movie, OverallRate / Views);
        }

        var moviesOrderedByRating = moviesRate
            .OrderByDescending(m => m.Value)
            .Select(m => m.Key)
            .ToList();
        return moviesOrderedByRating;
    }
}