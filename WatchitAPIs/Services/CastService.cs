using Microsoft.EntityFrameworkCore;
using WatchitAPIs.DTOs;
using WatchitAPIs.Repository;
using WatchitAPIs.Services_Contract;
using WatchitAPIs.Models;
using WatchitAPIs.EFCore;

namespace WatchitAPIs.Services
{
    public class CastService : GenericRepository<Cast>, ICastService
    {
        private readonly AppDbContext context;

        public CastService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        
        public  async Task<List<CastWithMoviesDTO>> GetCastAsync(string Name)
        {
            var Cast = await context.Casts
                .Where(c => (c.FirstName + c.LastName).Contains(Name))
                .Include(c=>c.Movies)
                .ThenInclude(m=>m.Movie).ToListAsync();
            var castWithMoviesDTO = new List<CastWithMoviesDTO>();
            if (Cast != null)
            {
                foreach (var cast in Cast)
                {
                    var castDto = new CastWithMoviesDTO();
                    castDto.Id = cast.Id;
                    castDto.FirstName = cast.FirstName;
                    castDto.LastName = cast.LastName;
                    castDto.Gender = cast.Gender;
                    castDto.Age = cast.Age;
                    castDto.Nationality = cast.Nationality;
                    foreach (var movie in cast.Movies)
                    {
                        castDto.Movies.Add(new MovieCastDirectorDTO()
                        {
                            Id = movie.Movie.Id,
                            Title = movie.Movie.Title
                        });
                    }

                    castWithMoviesDTO.Add(castDto);
                }
            }
            return castWithMoviesDTO;
        }

        public async Task<CastWithMoviesDTO> GetCastWithMoviesAsync(int id)
        {
            var cast = await context.Casts
                .Include(c=>c.Movies)
                .ThenInclude(m=>m.Movie)
                .FirstOrDefaultAsync(c => c.Id == id);
            var castDto = new CastWithMoviesDTO();
            if (cast != null)
            {
                castDto.Id = cast.Id;
                castDto.FirstName = cast.FirstName;
                castDto.LastName = cast.LastName;
                castDto.Gender = cast.Gender;
                castDto.Age = cast.Age;
                castDto.Nationality = cast.Nationality;
                foreach (var movie in cast.Movies)
                {
                    castDto.Movies.Add(new MovieCastDirectorDTO()
                    {
                        Id = movie.Movie.Id,
                        Title = movie.Movie.Title
                    });
                }
            }
            return castDto;
        }
    }
}
