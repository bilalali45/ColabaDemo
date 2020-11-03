 
using System.Threading.Tasks;

namespace LosIntegration.Service.InternalServices
{
    public interface IByteWebConnectorService
    {
      


        Task<short> GetLoanStatusAsync(string fileDataId);
    }
}
