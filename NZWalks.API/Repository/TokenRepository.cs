using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            // Create Claims
            var cliams = new List<Claim>();
            cliams.Add(new Claim(ClaimTypes.Email, user.Email));

            // To Add Roles
            foreach (var role in roles)
            {
                cliams.Add(new Claim(ClaimTypes.Role, role));
            }

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);

            // Design Token
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"], // issuer => who is generate token (URl for webAPI)
                configuration["Jwt:Audience"], // audience => (Clients) website for Angular or ...
                cliams, // Data Of User and Role
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials // Token verify and trust
                );

            return new JwtSecurityTokenHandler().WriteToken(token); // convert secrit token to string 

        }
    }
}
