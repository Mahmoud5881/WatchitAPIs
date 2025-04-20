using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using WatchitAPIs.DTOs;
using WatchitAPIs.Models;
using WatchitAPIs.Services_Contract;

namespace WatchitAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IAuthService authService;
        private readonly ISubscriptionService subscriptionService;
        public AccountController(UserManager<AppUser> userManager,IAuthService authService,ISubscriptionService subscriptionService)
        {
            this.userManager = userManager;
            this.authService = authService;
            this.subscriptionService = subscriptionService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO newUser)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.UserName = newUser.UserName;
                user.PasswordHash = newUser.Password;
                user.Email = newUser.Email;
                user.Subscription = subscriptionService.CreateSub(newUser);
                await userManager.AddToRoleAsync(user,"User");

                IdentityResult result = await userManager.CreateAsync(user,newUser.Password);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                return BadRequest(result.Errors.ToList());
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO newUser)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(newUser.UserName);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, newUser.Password);
                    if(found)
                    {
                        //if (user.Subscription.EndDate <= DateTime.Now)
                        //{
                        //    await subscriptionService.DeleteAsync(user.Subscription);
                        //    return Unauthorized("Your Subscription is expired, please renew and try again");
                        //}

                        JwtSecurityToken token = await authService.CreateTokenAsync(user);

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expires = token.ValidTo
                        });
                    }
                }
                return Unauthorized("User Name or Password is incorrect, please try again");
            }
            return BadRequest(ModelState);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> PutUserInfo([FromRoute]string id,[FromBody]EditUserInfoDTO userInfo)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.UserName = userInfo.UserName;
                    user.Email = userInfo.Email;
                    user.PasswordHash = userInfo.Password;
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
                return BadRequest();
            }
            return BadRequest(ModelState);

        }
    }

}
