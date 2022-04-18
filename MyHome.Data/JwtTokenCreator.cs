using Microsoft.IdentityModel.Tokens;

using MyHome.Shared;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyHome.Data
{
    public class JwtTokenCreator : ITokenCreator<ApplicationUser>
    {
        private const string AuthenticationType = "boerrigter";

        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secretKey;
        private readonly int _daysToExpire;
        private readonly JwtSecurityTokenHandler _tokenHandler;


        public JwtTokenCreator(IConfiguration configuration)
        {
            IConfigurationSection section = configuration.GetSection("Security");
            if (section is null) throw new ArgumentNullException(nameof(configuration));

            _issuer = section.GetValue<string>("Issuer");
            _audience = section.GetValue<string>("Audience");
            _secretKey = section.GetValue<string>("SecretKey");
            _daysToExpire = section.GetValue<int>("DaysToExpire");
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string CreateToken(ApplicationUser user)
        {
            SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor(user);
            SecurityToken token = _tokenHandler.CreateToken(tokenDescriptor);
            return _tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor GetTokenDescriptor(ApplicationUser user)
        {
            return new SecurityTokenDescriptor()
            {
                Subject = GetIdentity(user),
                Expires = DateTime.UtcNow.AddDays(_daysToExpire),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(GetSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private static ClaimsIdentity GetIdentity(ApplicationUser user)
        {
            return new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            }, AuthenticationType);
        }

        private SymmetricSecurityKey GetSecurityKey()
        {
            byte[] keyBuffer = Encoding.ASCII.GetBytes(_secretKey);
            return new SymmetricSecurityKey(keyBuffer);
        }
    }
}