using System;

namespace RainMaker.Common.Extensions
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
    }
}