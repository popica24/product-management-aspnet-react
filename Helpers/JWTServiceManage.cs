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
    public class JWTServiceManage : IJWTTokenService
    {
        private readonly IConnectionString _connectionString;
        private readonly IConfiguration _configuration;
        public JWTServiceManage(IConnectionString connectionString, IConfiguration configuration)
        {
            _connectionString = connectionString;
            _configuration = configuration;
        }

        public JWTTokens Authenticate(string email, string password)
        {
            var sql = "SELECT DisplayName,UserName From dbo.Users WHERE Email = @Email AND Password = @Password";

            using var db = new SqlDataContext(_connectionString);

            var request = db.Connection.QueryFirstOrDefault<UserModel>(sql, new {Email = email, Password = password });
            if (request == null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tkey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenBody = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tkey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenBody);
            
            return new JWTTokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
