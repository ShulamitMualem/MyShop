using System.Security.Cryptography;
using System.Text;
using Entity;
using Repository.UserRepository;
namespace Services.UserService
{
    public class MyServices : IMyServices
    {
        IMyRepository repository;
        public MyServices(IMyRepository myRepository)
        {
            repository = myRepository;
        }

        public async Task<User> GetUserById(int id)
        {
            return await repository.GetUserById(id);
        }
        public async Task<User> CreateUser(User user)
        {
            if (CheckPassword(user?.Password) < 3)
                return null;

            // Generate a unique salt
            var salt = GenerateSalt();
            user.PasswordSalt = salt;

            // Hash the password with the salt
            user.Password = HashPassword(user.Password, salt);

            return await repository.CreateUser(user);
        }
        public async Task<User> UpDateUser(int id, User userToUpdate)
        {
            if (CheckPassword(userToUpdate?.Password) < 3)
                throw new Exception("סיסמה חלשה מדי");
           return await repository.UpDateUser(id, userToUpdate);
        }
        public async Task<User> Login(string userName, string password)
        {
            var user = await repository.Login(userName, password);
            if (user == null)
                return null;

            // Hash the input password with the stored salt
            var hashedPassword = HashPassword(password, user.PasswordSalt);

            // Compare the hashed password with the stored password
            return hashedPassword == user.Password ? user : null;
        }
        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;
        }
        private string GenerateSalt()
        {
            var saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var combinedBytes = Encoding.UTF8.GetBytes(password + salt);
                var hashBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
