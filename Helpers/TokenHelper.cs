using Business.Contracts;
using Business.Domain;
using Dapper;
using Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;

namespace WebAPI.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        private readonly IConnectionString _connectionString;
        private readonly IConfiguration _configuration;
        public TokenHelper(IConnectionString connectionString, IConfiguration configuration)
        {
            _connectionString = connectionString;
            _configuration = configuration;
        }

        public JWTTokens Create(LoginRequest request)
        {
            var sql = "EXEC dbo.GetUserByCredentials @Email = @Email, @Password = @Password;";

            using var db = new SqlDataContext(_connectionString);

            var isValid = db.Connection.QueryFirstOrDefault<UserModel>(sql, request);
            if (isValid == null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tkey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenBody = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,isValid.DisplayName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tkey), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"]
            };
            var token = tokenHandler.CreateToken(tokenBody);
            
        

            return new JWTTokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
