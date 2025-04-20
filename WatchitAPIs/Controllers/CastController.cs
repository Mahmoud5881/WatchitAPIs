using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchitAPIs.Models;
using WatchitAPIs.Services;
using WatchitAPIs.Services_Contract;

namespace WatchitAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService castService;

        public CastController(ICastService castService)
        {
            this.castService = castService;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> GetActorsBySearch([FromQuery]string Name)
        {
            var Cast = castService.GetCastAsync(Name);
            if(Cast != null)
                return Ok(Cast);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActor([FromRoute]int id)
        {
            var cast = await castService.GetCastWithMoviesAsync(id);
            if(cast != null)
                return Ok(cast);
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PostCast([FromBody]Cast cast)
        {
            if(ModelState.IsValid)
            {
                await castService.AddAsync(cast);
                return Created();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> PutCast([FromRoute]int id,[FromBody]  Cast newCast)
        {
            if (ModelState.IsValid)
            {
                var success = await castService.UpdateEntityAsync(id, newCast);
                if (success)
                    return Ok();
                else
                    ModelState.AddModelError("Error", "This Cast Doesn't Exist");
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCast([FromRoute] int id)
        {
            bool success = await castService.DeleteAsync(id);
            if (success)         
                return Ok(); 
            ModelState.AddModelError("Error", "This Cast Doesn't Exist");
            return BadRequest(ModelState);
        }
    }
}
