namespace Stock_Back.DAL.Utilities;
public static class DateUtilities
{
    public static IEnumerable<DateTime> DatesUntil(this DateTime startDate, DateTime endDate)
    {
        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
        {
            yield return date;
        }
    }
    public static int GetdaysInaRange(this DateTime startDate, DateTime endDate)
    {
        return GetdaysInaRange(startDate, endDate, false);
    }
    public static int GetdaysInaRange(this DateTime startDate, DateTime endDate, bool includeWeekendDays = false)
    {
        var daysInaRage = startDate.DatesUntil(endDate);
        return daysInaRage.Count() - (includeWeekendDays ? daysInaRage.Where(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday).Count() : 0);
    }
}