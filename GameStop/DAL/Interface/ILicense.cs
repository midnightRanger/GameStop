using GameStop.Models;

namespace GameStop.DAL.Interface;

public interface ILicense
{
    public Task addLicense(LicenseModel license);
    public void updateLicense(LicenseModel license);
    public LicenseModel deleteLicense(in int id);
    
    public bool checkLicense(int id);

    public Task<List<LicenseModel>> getLicenses();
    
    public Task<LicenseModel> getLicense(int id);

    public IQueryable<LicenseModel> getAll(); 
}