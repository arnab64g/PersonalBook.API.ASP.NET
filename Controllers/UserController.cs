using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBook.API.Model;
using PersonalBook.API.Services;

namespace PersonalBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserBase userBase)
        {
            var result = await userService.RegistrationAsync(userBase);

            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Post([FromBody] Login login)
        {
            var result = await userService.LoginAsync(login);

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            ApplicationUser? applicationUser = await userService.GetApplicationUserAsync(id);

            return Ok(applicationUser);
        }
    }
}
