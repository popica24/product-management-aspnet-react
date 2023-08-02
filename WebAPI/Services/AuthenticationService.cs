using Business.Contracts;
using Business.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.Models.Users;

namespace WebAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;


        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthorizeResponse? Authenticate(LoginRequest request)
        {
            if (!_userRepository.UserExists(request.Email)) return null;
            var encryptedPw = PasswordHelper.ComputePassssword(request.Password);

            var claims = _userRepository.GetNameClaim(request.Email,encryptedPw);

            if(claims == null) return null;

            var token = Create(claims);

            var x =  new AuthorizeResponse()
            {
                Email = claims.Email.Trim(),
                Username = claims.DisplayName.Trim(),
                Token = token.Token
            };

            return x;
        }

        public bool SignUp(SignupRequest request)
        {
            if (_userRepository.UserExists(request.Email)) return false;
            var encryptedPw = PasswordHelper.ComputePassssword(request.Password);
            return _userRepository.Insert(request.Username,request.Username,request.Email, encryptedPw);
        }

        private JwtToken? Create(UserCredidentials creds)
        {
           
            if (creds == null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tkey = Encoding.UTF8.GetBytes("BOtIroGZMrhSxbLlAURxJP9cghygHovu");
            var tokenBody = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,creds.DisplayName,ClaimTypes.Email,creds.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tkey), SecurityAlgorithms.HmacSha256Signature),
                Audience = "https://localhost:7207",
                Issuer = "https://localhost:7207"
            };
            var token = tokenHandler.CreateToken(tokenBody);



            return new JwtToken { Token = tokenHandler.WriteToken(token) };
        }

    }
}
