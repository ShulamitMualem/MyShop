using Entity;

namespace Services.UserService
{
    public interface IMyServices
    {
        int CheckPassword(string password);
        Task<User> CreateUser(User user);
        Task<User> GetUserById(int id);
        Task<User> Login(string userName, string password);
        Task<User> UpDateUser(int id, User userToUpdate);
    }
}