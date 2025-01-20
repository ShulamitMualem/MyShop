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
        MyShop328264650Context _dbcontext;
        public RatingRepository(MyShop328264650Context context)
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
