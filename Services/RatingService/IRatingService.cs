using Entities;

namespace Services.RatingService
{
    public interface IRatingService
    {
        Task<Rating> CreateRating(Rating rating);
    }
}