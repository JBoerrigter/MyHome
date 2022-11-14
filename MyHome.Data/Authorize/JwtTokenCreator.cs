using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MyHome.Shared;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyHome.Data.Authorize
{
    public class JwtTokenCreator : ITokenCreator<ApplicationUser>
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _daysToExpire;
        private readonly SigningCredentials _signingCredentials;

        public JwtTokenCreator(IOptions<SecurityModel> options)
        {
            _issuer = options.Value.Issuer;
            _audience = options.Value.Audience;
            _daysToExpire = options.Value.DaysToExpire;
            _signingCredentials = new(
                key: GetSecurityKey(options.Value.SecretKey),
                algorithm: SecurityAlgorithms.HmacSha256);
        }

        public string CreateToken(ApplicationUser user)
        {
            IEnumerable<Claim> claims = GetClaims(user);
            JwtSecurityToken token = new(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(_daysToExpire),
                signingCredentials: _signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static IEnumerable<Claim> GetClaims(ApplicationUser user)
        {
            List<Claim> claims = new();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            if (!string.IsNullOrEmpty(user.UserName)) claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            if (!string.IsNullOrEmpty(user.Email)) claims.Add(new Claim(ClaimTypes.Email, user.Email));
            return claims;
        }

        private static SymmetricSecurityKey GetSecurityKey(string key)
        {
            byte[] keyBuffer = Encoding.ASCII.GetBytes(key);
            return new SymmetricSecurityKey(keyBuffer);
        }
    }
}