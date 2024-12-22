using Entity;

namespace Repository.UserRepository
{
    public interface IMyRepository
    {
        Task<User> CreateUser(User user);
        Task<User> GetUserById(int id);
        Task<User> Login(string userName, string password);
        Task UpDateUser(int id, User userToUpdate);
    }
}