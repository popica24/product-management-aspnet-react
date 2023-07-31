using Business.Domain;

namespace Business.Contracts
{
    public interface IUserRepository
    {
        public bool UserExists(string email);
        public UserCredidentials GetNameClaim(string email,string password);
        public bool Insert(string displayName, string userName, string email, string password);
    }
}
