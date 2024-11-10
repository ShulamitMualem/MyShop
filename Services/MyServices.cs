using Entity;
using Repository;
namespace Services
{
    public class MyServices : IServices
    {
        IRepository repository;
        public MyServices(IRepository myRepository)
        {
            repository = myRepository;
        }

        public User GetUserById(int id)
        {
            return repository.GetUserById(id);
        }
        public User CreateUser(User user)
        {
            return repository.CreateUser(user);
        }
        public void UpDateUser(int id, User userToUpdate)
        {
            repository.UpDateUser(id, userToUpdate);
        }
        public User Login(string userName, string password)
        {
            return repository.Login(userName, password);
        }
        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;
        }
    }
}
