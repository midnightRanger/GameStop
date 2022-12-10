using GameStop.DAL.Interface;
using GameStop.Models;
using GameStop.Response;

namespace GameStop.Services;

public class ReviewService : IReviewService
{
    private readonly ILogger<ReviewService> _logger;
    private readonly IReview _reviewRepository;
    private readonly ApplicationContext _db;

    public ReviewService(IReview reviewRepository, ILogger<ReviewService> logger, ApplicationContext db)
    {
        _reviewRepository = reviewRepository;
        _logger = logger;
        _db = db;
    }

    public async Task<BaseResponse<bool>> AcceptReview(ReviewModel review)
    {
        try
        {
            _reviewRepository.updateReview(review);
            
            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK,
                Description = "Review was successfully updated"
            };
        }
        
        
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[AcceptReview]: {ex.Message}");
            return new BaseResponse<bool>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}