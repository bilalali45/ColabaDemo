using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IActivityLogService
    {
        Task InsertLog(string activityId, string activity);
        Task<string> GetActivityLogId(string loanId, string requestId, string docId);
    }
}
