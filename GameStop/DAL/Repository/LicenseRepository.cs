using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.DAL.Repository;

public class LicenseRepository : ILicense
{
    
    private ApplicationContext _db = new();

    public LicenseRepository(ApplicationContext db)
    {
        _db = db; 
    }


    public async Task addLicense(LicenseModel license)
    {
        _db.License.Add(license);
        await _db.SaveChangesAsync();
    }

    public void updateLicense(LicenseModel license)
    {
        _db.Entry(license).State = EntityState.Modified;
        _db.SaveChanges();
    }

    public LicenseModel deleteLicense(in int id)
    {
        LicenseModel? license = _db.License.Find(id);

        if (license != null)
        {
            _db.License.Remove(license);
            _db.SaveChanges();
            return license;
        }

        throw new ArgumentNullException();
    }

    public bool checkLicense(int id)
    {
        return _db.License.Any(l => l.Id == id);
    }

    public async Task<List<LicenseModel>> getLicenses()
    {
        return await _db.License.ToListAsync();
    }

    public async Task<LicenseModel> getLicense(int id)
    {
        LicenseModel? license = await _db.License.FindAsync(id);

        if (license != null)
        {
            return license;
        }

        throw new ArgumentNullException();
    }


    public IQueryable<LicenseModel> getAll()
    {
        return _db.License; 
    }
}