using GameStop.Models;

namespace GameStop.DAL.Interface;

public interface IAccount
{
    public Task addAccount(AccountModel account);
    public void updateAccount(AccountModel account);
    public AccountModel deleteAccount(in int id);
    
    public bool checkAccount(int id);

    public List<AccountModel> getAccounts();
    public AccountModel getAccount(int id);

    public IQueryable<AccountModel> getAll(); 
}