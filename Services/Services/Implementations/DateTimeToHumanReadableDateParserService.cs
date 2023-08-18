namespace CGPost.Services.Services.Implementations
{
    public class DateTimeToHumanReadableDateParserService : IDateTimeToHumanReadableDateParserService
    {
        private string TodayString => "Today";
        private string YesterdayString => "Yesterday";
        public string Parse(DateTime date)
        {
            if (IsToday(date))
            {
                return $"{TodayString}, {date.ToString("t")}";
            }

            if (IsYesterday(date))
            {
                return $"{YesterdayString}, {date.ToString("t")}";
            }

            if (ThisYear(date))
            {
                return $"{date.ToString("MMM d")}, {date.ToString("t")}";
            }

            return $"{date.ToString("d")} {date.ToString("t")}" ;
        }

        private bool ThisYear(DateTime date)
        {
            var now = DateTime.Now;
            if (now.Year == date.Year)
                return true;

            return false;
        }

        private bool IsYesterday(DateTime date)
        {
            var now = DateTime.Now.Date;
            var checkWith = date.Date;

            if(now.AddDays(-1)==checkWith)
                return true;

            return false;
        }

        private bool IsToday(DateTime date)
        {
            var now = DateTime.Now;
            if(now.Year == date.Year && now.Month == date.Month && now.Day == date.Day)
            {
                return true;
            }
            return false;
        }
    }
}
