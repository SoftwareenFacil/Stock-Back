namespace EIC_Back.BLL.Helpers
{
    public static class DateHelper
    {
        public static (DateTime startDate, DateTime endDate) GetFirstAndLastDayOfMonth(DateTime inputDate)
        {
            DateTime firstDayOfMonth = new(inputDate.Year, inputDate.Month, 1);
            DateTime lastDayOfMonth = new(inputDate.Year, inputDate.Month, DateTime.DaysInMonth(inputDate.Year, inputDate.Month));

            return (firstDayOfMonth.ToUniversalTime(), lastDayOfMonth.ToUniversalTime());
        }
    }
}
