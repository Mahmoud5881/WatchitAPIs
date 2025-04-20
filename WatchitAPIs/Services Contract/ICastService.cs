using WatchitAPIs.DTOs;
using WatchitAPIs.Repository_Contract;
using WatchitAPIs.Models;

namespace WatchitAPIs.Services_Contract
{
    public interface ICastService : IGenericRepository<Cast>
    {
        Task<List<CastWithMoviesDTO>> GetCastAsync(string Name);

        Task<CastWithMoviesDTO> GetCastWithMoviesAsync(int id);
    }
}
