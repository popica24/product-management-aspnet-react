using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using Microsoft.AspNetCore.Cors;
using WebAPI.Helpers;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthorizationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public ActionResult Authenticate([FromBody]LoginRequest request)
        {
            var user = _authenticationService.Authenticate(request);
            if (user == null || user.Token == null) return BadRequest("Invalid Credidentials");
             Response.Headers.Add("Authorization", "Bearer "+user.Token);
            return Ok(user);
        }

        [HttpPost("signup")]
        public ActionResult SignUp([FromBody]SignupRequest request)
        {
            if (ModelState.IsValid)
            {
                var computedPassword = PasswordHelper.ComputePassssword(request.Password);
                if (_authenticationService.SignUp(request))
                {
                    return Ok(); 
                }
                return BadRequest();
            }
            return BadRequest();
        }

    }
}
