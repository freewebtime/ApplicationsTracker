using ApplicationsTracker.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationsTracker.Controllers
{
    public class AuthControllerOld : Controller
    {
        [HttpPost("login")]
        public IActionResult Login(string email)
        {
            // TEMPORARY: no password, no OAuth
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, email),
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(JwtHelper.GetSecurityTokenSecret()));
                //Encoding.UTF8.GetBytes("THIS_IS_DEV_ONLY_SECRET_CHANGE_LATER"));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenString });
        }
    }
}
