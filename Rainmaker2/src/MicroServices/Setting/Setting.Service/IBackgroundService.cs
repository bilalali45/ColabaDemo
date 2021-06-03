using Setting.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Setting.Service
{
    public interface IBackgroundService
    {
        Task EmailReminderJob();
        Task LoanStatusJob();
        void RegisterJob();
        Task DispatchEmailJob();
    }
}
