using Entity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Linq;
namespace Repository
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
            User user = await _dbcontext.Users.FindAsync(id);
            //return user == null ? null : user;
            return user; //if its null- will return null
        }
        public async Task<User> CreateUser(User user)
        {
            var res= await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();
            return res;// - the created user with the id
            //return user;
        }
        public async Task UpDateUser(int id, User userToUpdate)//return user
        {
            User user = await _dbcontext.Users.FindAsync(id);
            user = userToUpdate;
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<User> Login(string userName, string password)
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(user => user.UserName == userName && user.Password == password);
        }

    }
}
