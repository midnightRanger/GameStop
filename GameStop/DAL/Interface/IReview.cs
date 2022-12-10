using GameStop.Models;

namespace GameStop.DAL.Interface;

public interface IReview
{
    public Task addReview(ReviewModel review);
    public void updateReview(ReviewModel review);
    public ReviewModel deleteReview(in int id);
    
    public bool checkReview(int id);

    public Task<List<ReviewModel>> getReviews();
    public Task<ReviewModel> getReview(int id);

    public IQueryable<ReviewModel> getAll(); 
}