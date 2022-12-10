using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.DAL.Repository;

public class ReviewRepository : IReview
{
    private readonly ApplicationContext _db;
    
    public ReviewRepository(ApplicationContext db)
    {
        _db = db;
    }

    public async Task addReview(ReviewModel review)
    {
        _db.Review.Add(review);
        await _db.SaveChangesAsync();
    }

    public void updateReview(ReviewModel review)
    {
        _db.Entry(review).State = EntityState.Modified;
        _db.SaveChanges();
    }

    public ReviewModel deleteReview(in int id)
    {
        ReviewModel? review = _db.Review.Find(id);

        if (review != null)
        {
            _db.Review.Remove(review);
            _db.SaveChanges();
            return review;
        }

        throw new ArgumentNullException();
    }

    public bool checkReview(int id)
    {
        return _db.Review.Any(r => r.Id == id);
    }

    public async Task<List<ReviewModel>> getReviews()
    {
        return await _db.Review.ToListAsync();
    }

    public async Task<ReviewModel> getReview(int id)
    {
        ReviewModel? review = await _db.Review.FindAsync(id);

        if (review != null)
        {
            return review;
        }

        throw new ArgumentNullException();
    }


    public IQueryable<ReviewModel> getAll()
    {
        return _db.Review; 
    }
}