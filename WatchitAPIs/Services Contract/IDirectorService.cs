using WatchitAPIs.DTOs;
using WatchitAPIs.Repository_Contract;
using WatchitAPIs.Models;

namespace WatchitAPIs.Services_Contract
{
    public interface IDirectorService : IGenericRepository<Director>
    {
        Task<List<DirectorWithMoviesDTO>> GetDirectors(string Name);
        
        Task<DirectorWithMoviesDTO> GetDirectorWithMoviesAsync(int id);
    }
}
