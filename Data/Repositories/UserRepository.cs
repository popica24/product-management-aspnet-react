using Business.Contracts;
using Business.Domain;
using Dapper;


namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionString _connectionString;
        public UserRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public UserCredidentials GetNameClaim(string email,string password)
        {
            var sql = "EXEC GetUserByCredidentials @Email = @Email, @Password = @Password;";
            using var db = new SqlDataContext(_connectionString);

            var claims = db.Connection.QueryFirstOrDefault<UserCredidentials>(sql, new { Email = email, Password = password});

            return claims;
        }

        public bool Insert(string DisplayName, string UserName, string Email, string encryptedPassword)
        {
            var createEntity = "Insert into Users values (@DisplayName,@UserName,@Email,@encryptedPassword); Select @@ROWCOUNT";
            using var db = new SqlDataContext(_connectionString);
            var result = db.Connection.QueryFirst<int>(createEntity, new { DisplayName, UserName, Email,  encryptedPassword });
            return result == 1;
        }

        public bool UserExists(string email)
        {
            var checkDuplicateMail = "Select Count(Email) from dbo.Users WHERE Email = @Email";

            using var db = new SqlDataContext(_connectionString);

            var emailValid = db.Connection.QueryFirstOrDefault<int>(checkDuplicateMail, new { Email = email });

            return emailValid >= 1;

        }

       
    }
}
