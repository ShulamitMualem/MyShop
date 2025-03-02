using Entity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Linq;
using Microsoft.Extensions.Logging;
namespace Repository.UserRepository
{
    public class MyRepository : IMyRepository
    {
        MyShop328264650Context _dbcontext;
        public MyRepository(MyShop328264650Context context)
        {
            _dbcontext = context;
        }
        public async Task<User> GetUserById(int id)
        {
            User user = await _dbcontext.Users.Include(user=>user.Orders).FirstOrDefaultAsync(user=>user.UserId==id);
            return user;
        }
        public async Task<User> CreateUser(User user)
        {
            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpDateUser(int id, User userToUpdate)
        {
            userToUpdate.UserId = id;
         _dbcontext.Users.Update(userToUpdate);
            await _dbcontext.SaveChangesAsync();
            return userToUpdate;
        }
        public async Task<User> Login(string userName, string password)
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(user => user.UserName == userName && user.Password == password);
        }

    }
}
