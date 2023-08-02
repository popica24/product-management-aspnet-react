using Business.Domain;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class LoginRequest
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
      
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        public User ToRequest(LoginRequest loginRequest)
        {
            return new User()
            {
                Email = loginRequest.Email,
            };
        }

    }
}
