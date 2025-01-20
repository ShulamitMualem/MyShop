using Entities;
using Entity;
using Repository.RatingRepository;
using Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RatingService
{
    public class RatingService : IRatingService
    {
        IRatingRepository repository;
        public RatingService(IRatingRepository myRepository)
        {
            repository = myRepository;
        }

        public async Task<Rating> CreateRating(Rating rating)
        {
            return await repository.CreateRating(rating);
        }
    }
}
