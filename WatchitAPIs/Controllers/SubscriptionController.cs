using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchitAPIs.Models;
using WatchitAPIs.Services_Contract;

namespace WatchitAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService subscriptionService;
        private readonly UserManager<AppUser> userManager;
        public SubscriptionController(ISubscriptionService subscriptionService,UserManager<AppUser> userManager)
        {
            this.subscriptionService = subscriptionService;
            this.userManager = userManager;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserSubscription([FromQuery]string id)
        {
            AppUser? user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                Subscription? subscription = await subscriptionService.GetByIdAsync(user.SubscriptionId);
                if(subscription != null)
                    return Ok(subscription);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PostSubscribe([FromRoute]string id,[FromBody]Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByIdAsync(id);
                User.Subscription = subscription;
                await subscriptionService.UpdateAsync(User.Subscription);
                return Created();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            var subs = await subscriptionService.GetAllAsync();
            if(subs != null)
                return Ok(subs);
            return BadRequest();
        }
    }
}
