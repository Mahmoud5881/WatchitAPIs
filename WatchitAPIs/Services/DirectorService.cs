using Microsoft.EntityFrameworkCore;
using WatchitAPIs.DTOs;
using WatchitAPIs.Repository;
using WatchitAPIs.Services_Contract;
using WatchitAPIs.Models;
using WatchitAPIs.EFCore;

namespace WatchitAPIs.Services
{
    public class DirectorService : GenericRepository<Director>, IDirectorService
    {
        private readonly AppDbContext context;

        public DirectorService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<DirectorWithMoviesDTO>> GetDirectors(string Name)
        {
            var Director = await context.Directors
                .Where(c => (c.FirstName + c.LastName).Contains(Name))
                .Include(c=>c.Movies)
                .ToListAsync();
            var directorWithMoviesDTO = new List<DirectorWithMoviesDTO>();
            if (Director != null)
            {
                foreach (var director in Director)
                {
                    var directorDto = new DirectorWithMoviesDTO();
                    directorDto.Id = director.Id;
                    directorDto.FirstName = director.FirstName;
                    directorDto.LastName = director.LastName;
                    directorDto.Gender = director.Gender;
                    directorDto.Age = director.Age;
                    directorDto.Nationality = director.Nationality;
                    foreach (var movie in director.Movies)
                    {
                        directorDto.Movies.Add(new MovieCastDirectorDTO()
                        {
                            Id = movie.Id,
                            Title = movie.Title
                        });
                    }

                    directorWithMoviesDTO.Add(directorDto);
                }
            }
            return directorWithMoviesDTO;
        }
        
        public async Task<DirectorWithMoviesDTO> GetDirectorWithMoviesAsync(int id)
        {
            var director = await context.Directors
                .Include(c=>c.Movies)
                .FirstOrDefaultAsync(c => c.Id == id);
            var directorDto = new DirectorWithMoviesDTO();
            if (director != null)
            {
                directorDto.Id = director.Id;
                directorDto.FirstName = director.FirstName;
                directorDto.LastName = director.LastName;
                directorDto.Gender = director.Gender;
                directorDto.Age = director.Age;
                directorDto.Nationality = director.Nationality;
                foreach (var movie in director.Movies)
                {
                    directorDto.Movies.Add(new MovieCastDirectorDTO()
                    {
                        Id = movie.Id,
                        Title = movie.Title
                    });
                }
            }
            return directorDto;
        }
    }
}
