using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.DAL.Repository;

public class PlatformRepository: IPlatform
{

    private ApplicationContext _db;

    public PlatformRepository(ApplicationContext db)
    {
        _db = db;
    }

    public async Task addPlatform(PlatformModel platform)
    {
        await _db.Platform.AddAsync(platform);
        await _db.SaveChangesAsync();
    }

    public void updatePlatform(PlatformModel platform)
    {
        _db.Entry(platform).State = EntityState.Modified;
        _db.SaveChanges();
    }

    public PlatformModel deletePlatform(in int id)
    {
        PlatformModel? platform = _db.Platform.Find(id);

        if (platform != null)
        {
            _db.Platform.Remove(platform);
            _db.SaveChanges();
            return platform;
        }

        throw new ArgumentNullException();
    }

    public bool checkPlatform(int id)
    {
        return _db.Platform.Any(o => o.Id == id);
    }

    public async Task<List<PlatformModel>> getPlatforms()
    {
        return await _db.Platform.ToListAsync();
    }

    public async Task<PlatformModel> getPlatform(int id)
    {
        PlatformModel? platform = await _db.Platform.FindAsync(id);

        if (platform != null)
        {
            return platform;
        }

        throw new ArgumentNullException();
    }

    public IQueryable<PlatformModel> getAll()
    {
        return _db.Platform;
    }
}