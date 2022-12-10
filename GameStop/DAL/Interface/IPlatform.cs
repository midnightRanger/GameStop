using GameStop.Models;

namespace GameStop.DAL.Interface;

public interface IPlatform
{
    public Task addPlatform(PlatformModel platform);
    public void updatePlatform(PlatformModel platform);
    public PlatformModel deletePlatform(in int id);
    
    public bool checkPlatform(int id);

    public Task<List<PlatformModel>> getPlatforms();
    public Task<PlatformModel> getPlatform(int id);

    public IQueryable<PlatformModel> getAll(); 
}