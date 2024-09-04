using AMMAAPI.Models;
using AMMAAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMMAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService = new AuthService();
        public UserController(UserService userService) =>
            _userService = userService;

        [HttpPost("auth")]
        public async Task<IActionResult> Authenticate([FromBody] User u)
        {
            var user = await _userService.Authenticate(u.Email, u.Password);
            
            if (user == null)
            {
                return BadRequest(new { message = "Email is incorrect" });
            }

            if (user.Password == null)
            {
                return BadRequest(new { message = "Password is incorrect" });
            }

            user.Token = _authService.CreateJwt(user);

            await _userService.UpdateLastActiveAsync(user.UserId);

            return Ok(new
            {
                User = new
                {
                    user.UserId,
                    user.Name,
                    user.Email,
                    user.ContactNumber,
                    user.Role
                },
                Token = user.Token,
                Message = "Login successfully!",
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post(User u)
        {
            if (await _userService.CheckUserNameExist(u.Email))
            {
                return BadRequest(new { message = "Email already exist" });
            }

            var passMsg = _userService.CheckPasswordStrength(u.Password);
            if(!string.IsNullOrEmpty(passMsg))
            {
                return BadRequest(new { message = passMsg.ToString() });
            }

            u.LastActive = DateTime.UtcNow;

            await _userService.CreateAsync(u);

            return Ok(new
            {
                Message = "User Registred!"
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get() =>
            await _userService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var u = await _userService.GetByIdAsync(id);

            if (u == null)
            {
                return NotFound();
            }

            return u;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, User uIn)
        {
            var u = _userService.GetByIdAsync;

            if (u == null)
            {
                return NotFound();
            }

            await _userService.UpdateAsync(uIn);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, User uIn)
        {
            var u = _userService.GetByIdAsync(id);

            if (u == null)
            {
                return NotFound();
            }

            await _userService.RemoveAsync(uIn);

            return Ok();
        }


    }
}
