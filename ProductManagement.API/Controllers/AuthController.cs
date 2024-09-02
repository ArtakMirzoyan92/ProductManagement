using BusinessLayer.Constants;
using BusinessLayer.IServices;
using BusinessLayer.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private IUsersService _usersService;       

        public AuthController(IUsersService usersService)
        {
            _usersService = usersService;
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequest createUser)
        {
            UserResponse response = await _usersService.AddUserAsync(createUser);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequest user)
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
