namespace UseCases.Handlers.Authorization
{
    using Entities;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JwtGenerator: IJwtGenerator
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public JwtGenerator(JwtConfiguration configuration)
        {
            _jwtConfiguration = configuration;
        }

        public string CreateAccessToken(User user, string role)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtClaimsTypes.Id, user.Id),
                    new Claim(JwtClaimsTypes.Role, role)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var jwt = new JwtSecurityToken(
                    issuer: _jwtConfiguration.Issuer,
                    audience: _jwtConfiguration.Audience,
                    notBefore: DateTime.Now,
                    claims: claims,
                    expires: DateTime.Now.Add(TimeSpan.FromDays(_jwtConfiguration.DurationDays)),
                    signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string CreateRefreshToken()
        {
            return "refresh_token";
        }
    }
}
