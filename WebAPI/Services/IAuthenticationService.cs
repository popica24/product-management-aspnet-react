using WebAPI.Models;
using WebAPI.Models.Users;

namespace WebAPI.Services
{
    public interface IAuthenticationService
    {
        public AuthorizeResponse? Authenticate(LoginRequest request);
        public bool SignUp(SignupRequest request);

    }
}
