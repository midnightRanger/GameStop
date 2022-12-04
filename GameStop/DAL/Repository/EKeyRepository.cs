using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.DAL.Repository;

public class EKeyRepository : IEkey
{
    
    private ApplicationContext _db = new();

    public EKeyRepository(ApplicationContext db)
    {
        _db = db; 
    }


    public async Task addEkey(EKeyModel ekey)
    { 
        _db.EKey.Add(ekey);
        await _db.SaveChangesAsync();
    }

    public async Task updateEkey(EKeyModel ekey)
    {
        _db.Entry(ekey).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }
    public async Task updateEkeyInLoop(EKeyModel ekey)
    {
        _db.Entry(ekey).State = EntityState.Modified;
    }

    public EKeyModel deleteEkey(string key)
    {
        EKeyModel? ekey = _db.EKey.Find(key);

        if (ekey != null)
        {
            _db.EKey.Remove(ekey);
            _db.SaveChanges();
            return ekey;
        }

        throw new ArgumentNullException();
    }

    public bool checkEkey(string key)
    {
        return _db.EKey.Any(e => e.Key == key);
    }

    public async Task<List<EKeyModel>> getEkeys()
    {
        return await _db.EKey.ToListAsync();
    }

    public async Task<EKeyModel> getEkey(string key)
    {
        EKeyModel? ekey = await _db.EKey.FindAsync(key);

        if (ekey != null)
        {
            return ekey;
        }

        throw new ArgumentNullException();
    }

    public IQueryable<EKeyModel> getAll()
    {
        return _db.EKey;
    }
}