using Entity;

namespace Services
{
    public interface IServices
    {
        int CheckPassword(string password);
        User CreateUser(User user);
        User GetUserById(int id);
        User Login(string userName, string password);
        void UpDateUser(int id, User userToUpdate);
    }
}