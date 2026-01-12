using ApplicationsTracker.Data;
using ApplicationsTracker.Dtos;
using ApplicationsTracker.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace ApplicationsTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AuthController(AppDbContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpGet("auth-test")]
        public IActionResult AuthTest()
        {
            return Ok(new
            {
                User.Identity?.IsAuthenticated,
                Claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }

        [HttpPost("register-guest")]
        public async Task<IActionResult> RegisterGuest([FromBody] GuestRegistrationRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { error = "Email and password cannot be empty" });
            }

            var isUserExists = _db.Users.Any(u => u.Email == request.Email);
            if (isUserExists)
            {
                return BadRequest(new { error = "User already exists" });
            }

            var base64Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));
            var hashedPassword = PasswordHasherHelper.HashPassword(base64Password);
            //var hashedPassword = PasswordHasherHelper.HashPassword(request.Password);
            var userName = request.Name ?? "Guest";
            var user = new Entities.User
            {
                Email = request.Email,
                Name = userName,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = hashedPassword
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            var tokenString = JwtHelper.GenerateTokenString(user.Email, user.Name);

            return Ok(new { token = tokenString });
        }

        [HttpPost("login-guest")]
        public async Task<IActionResult> GuestLogin([FromBody] GuestLoginRequestDto request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return Unauthorized(new { error = "no such user found" });

            var base64Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));

            var isPasswordValid = PasswordHasherHelper.VerifyPassword(user.PasswordHash, base64Password);
            //var isPasswordValid = PasswordHasherHelper.VerifyPassword(request.Password, user.PasswordHash);
            if (!isPasswordValid)
                return Unauthorized(new { error = "invalid password" });

            var tokenString = JwtHelper.GenerateTokenString(user.Email, user.Name);

            return Ok(new { token = tokenString });
        }

        [HttpGet("login-google")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = "/auth/google-response" };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return Unauthorized();

            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value ?? "Google User";

            var tokenString = JwtHelper.GenerateTokenString(email, name);

            return Ok(new { tokenString });
        }
    }
}
