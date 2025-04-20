using Microsoft.AspNetCore.Mvc;
using WatchitAPIs.Models;
using WatchitAPIs.Services_Contract;

namespace WatchitAPIs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordsController : ControllerBase
{
    private readonly IRecordService recordService;
    public RecordsController(IRecordService recordService)
    {
        this.recordService = recordService;
    }
    [HttpPost]
    public async Task<IActionResult> PostRecord([FromBody]UserWatchRecord record)
    {
        if (ModelState.IsValid)
        {
            await recordService.AddAsync(record);
            return Created();
        }
        return BadRequest(ModelState);
    }
    [HttpGet("MovieRecord")]
    public async Task<IActionResult> GetMovieRecord([FromRoute]int movieId)
    {
        var Record = await recordService.GetMovieRecord(movieId);
        if (Record != null)
            return Ok(Record);
        return BadRequest();
    }
}