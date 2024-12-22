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
            return CheckPassword(user?.Password) < 3 ? null : await repository.CreateUser(user);
        }
        public async Task UpDateUser(int id, User userToUpdate)
        {
            if (CheckPassword(userToUpdate?.Password) < 3)
                throw new Exception("סיסמה חלשה מדי");
            await repository.UpDateUser(id, userToUpdate);
        }
        public async Task<User> Login(string userName, string password)
        {
            return await repository.Login(userName, password);
        }
        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;
        }
    }
}
