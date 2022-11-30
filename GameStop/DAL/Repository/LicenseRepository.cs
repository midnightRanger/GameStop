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
        try
        {
            _db.License.Add(license);
            await _db.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }

    public void updateLicense(LicenseModel license)
    {
        try
        {
            _db.Entry(license).State = EntityState.Modified;
            _db.SaveChanges();
        }
        catch
        {
            throw;
        }
    }

    public LicenseModel deleteLicense(in int id)
    {
        try
        {
            LicenseModel? license = _db.License.Find(id);

            if (license != null)
            {
                _db.License.Remove(license);
                _db.SaveChanges();
                return license;
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

    public bool checkLicense(int id)
    {
        return _db.License.Any(l => l.Id == id);
    }

    public async Task<List<LicenseModel>> getLicenses()
    {
        try
        {
            return await _db.License.ToListAsync();
        }
        catch
        {
            throw; 
        }
    }

    public async Task<LicenseModel> getLicense(int id)
    {
        try
        {
            LicenseModel? license = await _db.License.FindAsync(id);

            if (license != null)
            {
                return license;
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


    public IQueryable<LicenseModel> getAll()
    {
        return _db.License; 
    }
}