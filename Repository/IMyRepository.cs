using Entity;

namespace Repository
{
    public interface IMyRepository
    {
        User CreateUser(User user);
        Task<User> GetUserById(int id);
        Task<User> Login(string userName, string password);
        Task UpDateUser(int id, User userToUpdate);
    }
}