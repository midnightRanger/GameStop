using GameStop.Models;

namespace GameStop.DAL.Interface;

public interface IUser
{
    public Task addUser(UserModel user);
    public void updateUser(UserModel user);
    public UserModel deleteUser(in int id);
    
    public bool checkUser(int id);

    public List<UserModel> getUsers();
    public UserModel getUser(int id);
    
    public IQueryable<UserModel> getAll(); 
}