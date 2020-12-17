using DocManager.Model;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public interface ISettingService
    {
        Task<Setting> GetSetting();
        Task<LockSetting> GetLockSetting();
    }
}
