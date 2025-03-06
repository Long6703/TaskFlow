using Application.Contracts.Identity;
using Domain.Entities;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.JwtService
{
    public class TokenService : ITokenService
    {
        private readonly TaskFlowContext _context;
        private readonly JwtSettings _jwtSettings;

        public TokenService(TaskFlowContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        public (string AccessToken, string RefreshToken) GenerateTokens(Guid userId, string email, IEnumerable<string> permissions)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim("Permissions", string.Join(",", permissions))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: creds
            );

            var refreshToken = Guid.NewGuid().ToString();
            _context.RefreshTokens.Add(new RefreshToken
            {
                UserId = userId,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            });
            _context.SaveChanges();

            return (new JwtSecurityTokenHandler().WriteToken(accessToken), refreshToken);
        }

        public async Task<string> RefreshTokenAsync(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == refreshToken && !t.IsRevoked && t.ExpiresAt > DateTime.UtcNow);
            if (token == null) throw new UnauthorizedAccessException("Invalid or expired refresh token");

            var user = await _context.Users.FindAsync(token.UserId);
            if (user == null) throw new UnauthorizedAccessException("User not found");

            token.IsRevoked = true;
            _context.SaveChanges();

            // Create new tokens
            //var (accessToken, newRefreshToken) = GenerateTokens(user.Id, user.Username, user.Role, GetUserPermissions(user.Id));
            //return accessToken;
            return "";
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == refreshToken);
            if (token != null)
            {
                token.IsRevoked = true;
                _context.SaveChanges();
            }
        }
    }
}
