using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchitAPIs.Models;
using WatchitAPIs.Services;
using WatchitAPIs.Services_Contract;

namespace WatchitAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService directorService;

        public DirectorController(IDirectorService directorService)
        {
            this.directorService = directorService;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> GetDirectorsBySearch([FromQuery]string Name)
        {
            var Director = await directorService.GetDirectors(Name);
            if(Director != null)
                return Ok(Director);
            return BadRequest();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirector([FromRoute]int id)
        {
            var Director = await directorService.GetDirectorWithMoviesAsync(id);
            if(Director != null)
                return Ok(Director);
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PostDirector([FromBody] Director director)
        {
            if (ModelState.IsValid)
            {
                await directorService.AddAsync(director);
                return Created();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> PutDirector([FromRoute] int id, [FromBody] Director newDirector)
        {
            if (ModelState.IsValid)
            {
                var success = await directorService.UpdateEntityAsync(id, newDirector);
                if (success)
                    return Ok();
                else
                    ModelState.AddModelError("Error", "This Director Doesn't Exist");
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDirector([FromRoute] int id)
        {
            bool success = await directorService.DeleteAsync(id);
            if (success)
                return Ok();
            ModelState.AddModelError("Error", "This Director Doesn't Exist");
            return BadRequest(ModelState);
        }
    }
}
