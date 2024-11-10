using Entity;

namespace Repository
{
    public interface IRepository
    {
        User CreateUser(User user);
        User GetUserById(int id);
        User Login(string userName, string password);
        void UpDateUser(int id, User userToUpdate);
    }
}