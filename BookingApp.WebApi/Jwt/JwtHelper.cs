using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingApp.WebApi.Jwt // Namespace for JWT-related functionality
{
    public static class JwtHelper // Static class for generating JWT tokens
    {
        public static string GenerateJwtToken(JwtDto jwtInfo) // Method to generate a JWT token
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.SecretKey)); // Create security key from secret key

            // Credentials for signing the token
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Define the claims to include in the token
            var claims = new[]
            {
                new Claim(JwtClaimNames.Id, jwtInfo.Id.ToString()), // User ID claim
                new Claim(JwtClaimNames.Email, jwtInfo.Email), // User email claim
                new Claim(JwtClaimNames.FirstName, jwtInfo.FirstName), // User first name claim
                new Claim(JwtClaimNames.LastName, jwtInfo.LastName), // User last name claim
                new Claim(JwtClaimNames.UserType, jwtInfo.UserType.ToString()), // User type claim
                new Claim(ClaimTypes.Role, jwtInfo.UserType.ToString()) // User role claim
            };

            var expireTime = DateTime.Now.AddMinutes(jwtInfo.ExpireMinutes); // Token expiration time

            // Token descriptor containing all the information for the token
            var tokenDescriptor = new JwtSecurityToken(jwtInfo.Issuer, jwtInfo.Audience, claims, null, expireTime, credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor); // Generate the token

            return token; // Return the generated token
        }
    }
}
