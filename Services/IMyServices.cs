using Entity;

namespace Services
{
    public interface IMyServices
    {
        int CheckPassword(string password);
        Task<User> CreateUser(User user);
        Task<User> GetUserById(int id);
        Task<User> Login(string userName, string password);
        Task UpDateUser(int id, User userToUpdate);
    }
}