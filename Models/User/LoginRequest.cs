using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class LoginRequest
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
      //  [DataType(DataType.Password)
        public string Password { get; set; }
    }
}
