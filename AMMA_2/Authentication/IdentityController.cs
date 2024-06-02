/*using AMMAAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AMMAAPI.Authentication
{
    public class IdentityController : ControllerBase
    {
        private const string TokenSecret = "AeonMallIndoorNavigationAppAeonMallIndoorNavigationApp";
        private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8);

        [HttpPost("createLoginToken")]
        public Task<IActionResult> createLoginToken([FromBody] User model)
        {
            if (model == null)
            {
                return Task.FromResult<IActionResult>(BadRequest("Invalid client request"));
            }

            if (model.Role == "admin" || model.Role == "user")
            {
                // check the email and password from the database
                // if the email and password are correct, then create a token


                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSecret));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, model.Role)
                };

                var tokeOptions = new JwtSecurityToken(
                    issuer: "AMMA",
                    audience: "AMMA",
                    claims: claims,
                    expires: DateTime.Now.Add(TokenLifeTime),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Task.FromResult<IActionResult>(Ok(new { Token = tokenString }));
            }
            else
            {
                return Task.FromResult<IActionResult>(Unauthorized());
            }
        }

        [HttpPost("changePasswordToken")]
        public Task<IActionResult> changePasswordToken([FromBody] Login model)
        {
            if (model == null)
            {
                return Task.FromResult<IActionResult>(BadRequest("Invalid client request"));
            }

            if (model.Username == "admin" && model.Password == "admin")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSecret));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, model.Role)
                };

                var tokeOptions = new JwtSecurityToken(
                    issuer: "AMMA",
                    audience: "AMMA",
                    claims: claims,
                    expires: DateTime.Now.Add(TokenLifeTime),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Task.FromResult<IActionResult>(Ok(new { Token = tokenString }));
            }
            else
            {
                return Task.FromResult<IActionResult>(Unauthorized());
            }
        }
    }

}
*/