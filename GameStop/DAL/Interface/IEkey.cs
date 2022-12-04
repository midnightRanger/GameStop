using GameStop.Models;

namespace GameStop.DAL.Interface;

public interface IEkey
{
    public Task addEkey(EKeyModel ekey);
    public Task updateEkey(EKeyModel ekey);
    public Task updateEkeyInLoop(EKeyModel ekey);
    public EKeyModel deleteEkey(string key);
    
    public bool checkEkey(string key);

    public Task<List<EKeyModel>> getEkeys();
    public Task<EKeyModel> getEkey(string key);

    public IQueryable<EKeyModel> getAll(); 
}