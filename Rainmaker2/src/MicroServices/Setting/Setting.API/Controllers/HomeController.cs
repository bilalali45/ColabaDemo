using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Setting.Service;

namespace Setting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IEmailReminderService emailReminderService;
        public HomeController(IEmailReminderService emailReminderService)
        {
            this.emailReminderService = emailReminderService;
        }

        

        [Route("/")]
        [Route("[action]")]
        public string Index()
        {
            return "Setting micro service is running";
        }

        [HttpGet(template: "TriggerJob")]
        public string TriggerJob()
        {
            this.emailReminderService.JobTrigger();
            return "Job Queued Successfully";
        }


    }
}
