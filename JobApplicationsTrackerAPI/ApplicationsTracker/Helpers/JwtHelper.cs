using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationsTracker.Helpers
{
    public static class JwtHelper
    {
        public static JwtSecurityToken GenerateToken(string email, string name)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetSecurityTokenSecret()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, name),
                //new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email)
            };

            var token = new JwtSecurityToken(
                //issuer: "yourapi",
                //audience: "yourapi",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return token;
        }

        public static string GenerateTokenString(string email, string name)
        {
            var token = GenerateToken(email, name);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GetSecurityTokenSecret()
        {
            return "THIS_IS_DEV_ONLY_SECRET_CHANGE_LATER";
            //return "MY_SECRET_KEY_MY_SECRET_KEY_MY_SECRET_KEY_MY_SECRET_KEY";
        }
    }
}
