using System.Threading.Tasks;

namespace DocManager.Service
{
    public interface IActivityLogService
    {
        Task InsertLog(string activityId, string activity);
        Task<string> GetActivityLogId(string loanId, string requestId, string docId);
    }
}
