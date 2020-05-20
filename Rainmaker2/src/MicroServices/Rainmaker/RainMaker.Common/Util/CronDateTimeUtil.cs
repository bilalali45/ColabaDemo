using Quartz;
using RainMaker.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RainMaker.Common.Util
{
    public class CronDateTimeUtil
    {
        public List<string> GetDateTimeFromCornExpression(string cronText, string timeZone)
        {
            var cornExp = cronText;//"0 */3 7-15 ? * MON,TUE,WED,THU,FRI";
            var isValidExpression = CronExpression.IsValidExpression(cornExp);
            var dateTimeArray = new List<string>();

            if (isValidExpression)
            {
                var timeList = ParseCron(cornExp, timeZone);

                foreach (var itm in timeList)
                {
                    dateTimeArray.Add(itm.Value.DateTime.ToString("dddd MM-dd-yyyy hh:mm:ss tt"));
                }
            }
            else
            {
                dateTimeArray.Add("Invalid expression");
            }

            return dateTimeArray;
        }

        private static IEnumerable<DateTimeOffset?> ParseCron(string cronExpression, string timeZone)
        {
            if (string.IsNullOrEmpty(cronExpression)) throw new ArgumentException("cronExpression");
            var result = new List<DateTimeOffset?>();
            var quartzHelper = new CronExpression(cronExpression);
            var destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            quartzHelper.TimeZone = destinationTimeZone;
            DateTimeOffset dt = DateTime.UtcNow;
            var numberOfDays = 0;
            var numberOfSchedules = 500;
            var restrictToDays = 30;

            while (numberOfDays <= restrictToDays)
            {
                var nextScheduledJob = quartzHelper.GetNextValidTimeAfter(dt);

                if (nextScheduledJob != null)
                {
                    dt = nextScheduledJob.Value;
                    if (result.Count != 0)
                    {
                        var lastDate = result.Last();
                        var diff = dt.Day - lastDate.Value.Day;
                        if (diff >= 1)
                        {
                            numberOfDays++;
                        }
                    }
                    result.Add(CovertTimeZoneToUtc(nextScheduledJob.Value.DateTime, destinationTimeZone));
                    if (result.Count() == numberOfSchedules)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        private static DateTime CovertTimeZoneToUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
        {
            return dateTime.ConvertUtcToLocal(destinationTimeZone);
        }
    }
}
