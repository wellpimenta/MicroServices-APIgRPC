using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Grpc.Core;
using Microsoft.IdentityModel.Tokens;
using TaskGrpcApi.Protos;

namespace TaskGrpcApi.Services
{
    public class AuthService : AuthService.AuthServiceBase
    {
        private readonly string _jwtKey = "super_secret_key_123!"; // Should match Program.cs

        public override Task<AuthResponse> Authenticate(AuthRequest request, ServerCallContext context)
        {
            // For demo, accept only one user
            if (request.Username == "testuser" && request.Password == "password")
            {
                var token = GenerateJwtToken(request.Username);
                return Task.FromResult(new AuthResponse { Token = token });
            }

            throw new RpcException(new Status(StatusCode.Unauthenticated, "Invalid username or password"));
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "User")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}