using WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Contracts;
using WebAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenHelper _tokenHelper;

        public AuthorizationController(ITokenHelper tokenHelper, IAuthenticationService authenticationService)
        {
            _tokenHelper = tokenHelper;
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public ActionResult Authenticate([FromBody]LoginRequest request)
        {
            var validUser = _authenticationService.Authenticate(request.Email,request.Password);
            if (!validUser) return BadRequest("Invalid Credidentials");
            var token = _tokenHelper.Create(request);
             Response.Headers.Add("Authorization", "Bearer "+token.Token);
            return Ok(token.Token);
        }

    }
}
