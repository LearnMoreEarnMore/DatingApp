using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateServie(AppUser appUser)
        {
            //Create List of claims
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.NameId, appUser.Username)
            };

            //Create credentials
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds,
                Expires = DateTime.Now.AddDays(7)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}