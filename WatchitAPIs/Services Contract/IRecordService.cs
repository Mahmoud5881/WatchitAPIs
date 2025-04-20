using WatchitAPIs.Models;
using WatchitAPIs.Repository_Contract;

namespace WatchitAPIs.Services_Contract;

public interface IRecordService : IGenericRepository<UserWatchRecord>
{
    Task<List<UserWatchRecord>> GetMovieRecord(int movieId);
}