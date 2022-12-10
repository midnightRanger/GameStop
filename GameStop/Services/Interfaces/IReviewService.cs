using GameStop.Models;
using GameStop.Response;

namespace GameStop.DAL.Interface;

public interface IReviewService
{
    Task<BaseResponse<bool>> AcceptReview(ReviewModel review);
}