
using Business.Domain;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class SignupRequest
    {
        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    

        public User ToRequest(SignupRequest request)
        {
            return new User()
            {
                Email = Email.Trim(),
                UserName = Username.Trim(),
            };
        }

    }


}
