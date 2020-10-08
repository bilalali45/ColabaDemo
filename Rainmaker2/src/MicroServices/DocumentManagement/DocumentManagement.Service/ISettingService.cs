using DocumentManagement.Entity;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface ISettingService
    {
        Task<Setting> GetSetting();
    }
}
