using Entities;

namespace Repository.RatingRepository
{
    public interface IRatingRepository
    {
        Task<Rating> CreateRating(Rating newRating);
    }
}