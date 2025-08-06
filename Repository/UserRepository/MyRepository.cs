using Entity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Linq;
using Microsoft.Extensions.Logging;
using Repository.Exceptions;

namespace Repository.UserRepository
{
    public class MyRepository : IMyRepository
    {
        MyShopContext _dbcontext;
        public MyRepository(MyShopContext context)
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
            var userExist = await _dbcontext.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if (userExist != null)
            {
                throw new UserAlreadyExistsException("User with the same username already exists.");
            }
            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpDateUser(int id, User userToUpdate)
        {
            userToUpdate.UserId = id;
            var userExist = await _dbcontext.Users.FirstOrDefaultAsync(user => user.UserName == userToUpdate.UserName);
            if (userExist != null)
            {
                _dbcontext.Entry(userExist).State = EntityState.Detached;
            }
            if (userExist!=null&& userExist.UserId!=id)
            {
                throw new UserAlreadyExistsException("User with the same username already exists.");
            }
            _dbcontext.Users.Update(userToUpdate);
            await _dbcontext.SaveChangesAsync();
            return userToUpdate;
        }
        public async Task<User> Login(string userName, string password)
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(user => user.UserName == userName);
        }

    }
}
