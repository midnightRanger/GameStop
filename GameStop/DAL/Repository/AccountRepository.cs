using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.DAL.Repository;

public class AccountRepository : IAccount
{
    private ApplicationContext _db = new();

    public AccountRepository(ApplicationContext db)
    {
        _db = db; 
    }

    
    public async Task addAccount(AccountModel account)
    {
        try
        {
            _db.Account.Add(account);
            await _db.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }

    public void updateAccount(AccountModel account)
    {
        try
        {
            _db.Entry(account).State = EntityState.Modified;
            _db.SaveChanges();
        }
        catch
        {
            throw;
        }
    }

    public AccountModel deleteAccount(in int id)
    {
        try
        {
            AccountModel? account = _db.Account.Find(id);

            if (account != null)
            {
                _db.Account.Remove(account);
                _db.SaveChanges();
                return account;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        catch
        {
            throw;
        }
    }

    public bool checkAccount(in int id)
    {
        throw new NotImplementedException();
    }

    public bool checkAccount(int id)
    {
        return _db.Account.Any(e => e.Id == id);
    }

    public List<AccountModel> getAccounts()
    {
        try
        {
            return _db.Account.ToList();
        }
        catch
        {
            throw; 
        }
    }

    public AccountModel getAccount(int id)
    {
        try
        {
            // ReSharper disable once HeapView.BoxingAllocation
            AccountModel? account = _db.Account.Find(id);

            if (account != null)
            {
                return account;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        catch
        {
            throw;
        }
    }

    public IQueryable<AccountModel> getAll()
    {
        return _db.Account; 
    }
}