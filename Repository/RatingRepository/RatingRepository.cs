using Entities;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RatingRepository
{
    public class RatingRepository : IRatingRepository
    {
        MyShopContext _dbcontext;
        public RatingRepository(MyShopContext context)
        {
            _dbcontext = context;
        }
        public async Task<Rating> CreateRating(Rating newRating)
        {
            await _dbcontext.Ratings.AddAsync(newRating);
            await _dbcontext.SaveChangesAsync();
            return newRating;
        }
    }
}
