using Microsoft.EntityFrameworkCore;
using WatchitAPIs.EFCore;
using WatchitAPIs.Models;
using WatchitAPIs.Repository;
using WatchitAPIs.Services_Contract;

namespace WatchitAPIs.Services;

public class RecordService: GenericRepository<UserWatchRecord>,IRecordService
{
    private readonly AppDbContext context;

    public RecordService(AppDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<List<UserWatchRecord>> GetMovieRecord(int movieId) => 
        await context.Records.Where(m=>m.MovieId == movieId).ToListAsync();
}