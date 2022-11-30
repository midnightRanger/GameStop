using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.DAL.Repository;

public class UserRepository :IUser
{
    private readonly ApplicationContext _db = new();

    public UserRepository(ApplicationContext db)
    {
        _db = db; 
    }

    public List<UserModel> getUsers()
    {
        return _db.User.ToList();
    }

    public UserModel getUser(int id)
    {
        UserModel? user = _db.User.Find(id);
        if (user != null)
        {
            return user;
        }
        throw new ArgumentNullException();
    }

    public IQueryable<UserModel> getAll()
    {
        return _db.User; 
    }

    public async Task addUser(UserModel user)
    {
        await _db.User.AddAsync(user);
        await _db.SaveChangesAsync();
    }
    
    public void updateUser(UserModel user)
    {
        _db.Entry(user).State = EntityState.Modified;
        _db.SaveChanges();
    }
    
    public UserModel deleteUser(in int id)
    {
        UserModel? user = _db.User.Find(id);

        if (user != null)
        {
            _db.User.Remove(user);
            _db.SaveChanges();
            return user;
        }

        throw new ArgumentNullException();
    }
    
    public bool checkUser(int id)
    {
        return _db.User.Any(u => u.Id == id);
    }
}