using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Helpers
{
    public class TokenService : ITokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 60;
        private readonly IConfiguration _configuration;
        private string _key;
        private string _issuer;
        private string _audience;

        private readonly ISessionRepository _sessionRepository;

        public TokenService(IConfiguration configuration, ISessionRepository sessionRepository)
        {
            _configuration = configuration;
            _sessionRepository = sessionRepository;

            _key = _configuration["Jwt:Key"];
            _issuer = _configuration["Jwt:Issuer"];
            _audience = _configuration["Jwt:Audience"];
        }

        public string BuildToken(User user)
        {
            Guid nameIdentifierGuid = Guid.NewGuid();
            int expireInMinutes = Convert.ToInt32(_configuration["Jwt:ExpireInMinutes"]);
            DateTime expireDate = DateTime.UtcNow.AddMinutes(expireInMinutes);

            Claim[]? claims =
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()),
                new Claim(ClaimTypes.Sid,user.LdapUid),
                new Claim(ClaimTypes.NameIdentifier, nameIdentifierGuid.ToString("N"))
            };

            SymmetricSecurityKey? securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            SigningCredentials? credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature); //evtl das ohne Signature verwenden           
            JwtSecurityToken? tokenDescriptor = new JwtSecurityToken(_issuer, _audience, claims,
                expires: expireDate, signingCredentials: credentials);

            Session session = new Session
                              {Id = nameIdentifierGuid, LdapUser = user};
            _sessionRepository.Add(session);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public bool ValidateToken(string token)
        {
            byte[]? mySecret = Encoding.UTF8.GetBytes(_key);
            SymmetricSecurityKey? mySecurityKey = new SymmetricSecurityKey(mySecret);
            JwtSecurityTokenHandler? tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = _issuer,
                        ValidAudience = _audience,
                        IssuerSigningKey = mySecurityKey
                    }, out SecurityToken validatedToken);

                JwtSecurityToken? jwt = validatedToken as JwtSecurityToken;

                if (jwt?.ValidTo < DateTime.UtcNow)
                {
                    _sessionRepository.DeleteExpiredSessions();

                    return false;
                }

            }
            catch
            {
                return false;
            }

            return true;
        }

        public JwtSecurityToken? GetToken(string token)
        {
            byte[]? mySecret = Encoding.UTF8.GetBytes(_key);
            SymmetricSecurityKey? mySecurityKey = new SymmetricSecurityKey(mySecret);
            JwtSecurityTokenHandler? tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = _issuer,
                        ValidAudience = _audience,
                        IssuerSigningKey = mySecurityKey
                    }, out SecurityToken validatedToken);

                JwtSecurityToken? jwt = validatedToken as JwtSecurityToken;
                return jwt;
            }
            catch
            {
                throw new Exception("failed");
            }
        }

    }
}