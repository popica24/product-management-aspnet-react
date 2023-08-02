namespace WebAPI.Models.Users
{
    public class AuthorizeResponse
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
