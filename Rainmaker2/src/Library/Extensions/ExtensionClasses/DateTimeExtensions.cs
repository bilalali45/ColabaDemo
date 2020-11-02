using System;

namespace Extensions.ExtensionClasses
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUtc(this DateTime value)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dateTime: value);
        }


        public static DateTime UtcToTimeZone(this DateTime dateTime,
                                             string timeZone = "Central Standard Time")
        {
            var timezoneid = TimeZoneInfo.FindSystemTimeZoneById(id: timeZone.Trim());
            return dateTime.ConvertUtcToLocal(destinationTimeZone: timezoneid);
        }


        public static DateTime? UtcToTimeZone(this DateTime? dateTime,
                                              string timeZone = "Central Standard Time")
        {
            var timezoneid = TimeZoneInfo.FindSystemTimeZoneById(id: timeZone.Trim());
            return dateTime.ConvertUtcToLocal(destinationTimeZone: timezoneid);
        }


        public static DateTime TimeZoneToUtc(this DateTime dateTime,
                                             string timeZone = "Central Standard Time")
        {
            var timezoneid = TimeZoneInfo.FindSystemTimeZoneById(id: timeZone.Trim());
            return dateTime.ConvertLocalToUtc(sourceTimeZone: timezoneid);
        }


        public static DateTime? TimeZoneToUtc(this DateTime? dateTime,
                                              string timeZone = "Central Standard Time")
        {
            var timezoneid = TimeZoneInfo.FindSystemTimeZoneById(id: timeZone.Trim());
            return dateTime.ConvertLocalToUtc(sourceTimeZone: timezoneid);
        }


        public static DateTime? ConvertUtcToLocal(this DateTime? dt)
        {
            if (dt == null) return null;
            return dt.Value.ConvertUtcToLocal();
        }


        public static DateTime ConvertUtcToLocal(this DateTime dt)
        {
            dt = DateTime.SpecifyKind(value: dt,
                                      kind: DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTime(dateTime: dt,
                                            destinationTimeZone: TimeZoneInfo.Local);
        }


        public static DateTime? ConvertUtcToLocal(this DateTime? dt,
                                                  TimeZoneInfo destinationTimeZone)
        {
            if (dt == null) return null;
            return dt.Value.ConvertUtcToLocal(destinationTimeZone: destinationTimeZone);
        }


        public static DateTime ConvertUtcToLocal(this DateTime dt,
                                                 TimeZoneInfo destinationTimeZone)
        {
            dt = DateTime.SpecifyKind(value: dt,
                                      kind: DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTime(dateTime: dt,
                                            destinationTimeZone: destinationTimeZone);
        }


        public static DateTime? ConvertLocalToUtc(this DateTime? dt)
        {
            if (dt == null) return null;
            return dt.Value.ConvertLocalToUtc();
        }


        public static DateTime ConvertLocalToUtc(this DateTime dt)
        {
            dt = DateTime.SpecifyKind(value: dt,
                                      kind: DateTimeKind.Local);
            return TimeZoneInfo.ConvertTime(dateTime: dt,
                                            destinationTimeZone: TimeZoneInfo.Utc);
        }


        public static DateTime? ConvertLocalToUtc(this DateTime? dt,
                                                  TimeZoneInfo sourceTimeZone)
        {
            if (dt == null) return null;
            return dt.Value.ConvertLocalToUtc(sourceTimeZone: sourceTimeZone);
        }


        public static DateTime ConvertLocalToUtc(this DateTime dt,
                                                 TimeZoneInfo sourceTimeZone)
        {
            dt = DateTime.SpecifyKind(value: dt,
                                      kind: DateTimeKind.Unspecified);
            if (sourceTimeZone.IsInvalidTime(dateTime: dt))
                dt = dt.AddHours(value: 1);
            return TimeZoneInfo.ConvertTime(dateTime: dt,
                                            sourceTimeZone: sourceTimeZone,
                                            destinationTimeZone: TimeZoneInfo.Utc);
        }


        public static DateTime? SpecifyKind(this DateTime? dt,
                                            DateTimeKind kind)
        {
            if (dt.HasValue)
                dt = DateTime.SpecifyKind(value: dt.Value,
                                          kind: kind);

            return dt;
        }


        public class ElapsedTimeSpan
        {
            public int years;
            public int months;
            public int days;
            public int hours;
            public int minutes;
            public int seconds;
            public int milliseconds;
        }


        public static ElapsedTimeSpan ElapsedTime( this DateTime fromDate, DateTime toDate)
        {

            ElapsedTimeSpan elapsedTimeSpan;
            // If from_date > to_date, switch them around.
            if (fromDate > toDate)
            {
              elapsedTimeSpan =    ElapsedTime(toDate, fromDate);
                elapsedTimeSpan.years = -elapsedTimeSpan.years ;
                elapsedTimeSpan.months = -elapsedTimeSpan.months ;
                elapsedTimeSpan.days = -elapsedTimeSpan.days ;
                elapsedTimeSpan.hours = -elapsedTimeSpan.hours ;
                elapsedTimeSpan.minutes = -elapsedTimeSpan.minutes ;
                elapsedTimeSpan.seconds = -elapsedTimeSpan.seconds ;
                elapsedTimeSpan.milliseconds = -elapsedTimeSpan.milliseconds ;
            }
            else
            {
                elapsedTimeSpan = new ElapsedTimeSpan();
                // Handle the years.
                elapsedTimeSpan.years = toDate.Year - fromDate.Year;

                // See if we went too far.
                DateTime test_date = fromDate.AddMonths(12 * elapsedTimeSpan.years);
                if (test_date > toDate)
                {
                    elapsedTimeSpan.years--;
                    test_date = fromDate.AddMonths(12 * elapsedTimeSpan.years);
                }

                // Add months until we go too far.
                elapsedTimeSpan.months = 0;
                while (test_date <= toDate)
                {
                    elapsedTimeSpan.months++;
                    test_date = fromDate.AddMonths(12 * elapsedTimeSpan.years + elapsedTimeSpan.months);
                }
                elapsedTimeSpan.months--;

                // Subtract to see how many more days,
                // hours, minutes, etc. we need.
                fromDate = fromDate.AddMonths(12 * elapsedTimeSpan.years + elapsedTimeSpan.months);
                TimeSpan remainder = toDate - fromDate;
               elapsedTimeSpan.days = remainder.Days;
               elapsedTimeSpan.hours = remainder.Hours;
               elapsedTimeSpan.minutes = remainder.Minutes;
               elapsedTimeSpan.seconds = remainder.Seconds;
                elapsedTimeSpan.milliseconds = remainder.Milliseconds;
            }

            return elapsedTimeSpan;
        }


    }
}