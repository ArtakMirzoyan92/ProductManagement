using BusinessLayer.Constants;
using BusinessLayer.IServices;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUsersService _usersService;
        private IConfiguration _configuration;

        public UserController(IUsersService usersService, IConfiguration configuration)
        {
            _usersService = usersService;
            _configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto createUser)
        {
            await _usersService.AddUserAsync(createUser);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequest user)
        {
            string token = await _usersService.GetByEmailAsync(user.Email, user.PasswordHash);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(ErrorMessages.InvalidLoginOrPassword);
            }

            HttpContext.Response.Cookies.Append(CookieConstants.AuthTokenCookieName, token);

            return Ok(token);
        }               
    }
}
