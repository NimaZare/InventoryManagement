namespace Tools;

public static class Utility
{
    private static readonly PersianCalendar _persianCalendar = new();

    public static string ConvertToPersianDate(DateTime dateTime)
    {
        var day = _persianCalendar.GetDayOfMonth(time: dateTime);
        var month = _persianCalendar.GetMonth(time: dateTime);
        var year = _persianCalendar.GetYear(time: dateTime);

        var dayString = day.ToString().PadLeft(totalWidth: 2, paddingChar: '0');
        var monthString = month.ToString().PadLeft(totalWidth: 2, paddingChar: '0');
        var result = $"{year}/{monthString}/{dayString}";

        return result;
    }

    public static string ConvertToPersianDateTime(DateTime dateTime)
    {
        var date = ConvertToPersianDate(dateTime);
        var hour = dateTime.Hour;
        var minute = dateTime.Minute;
        var second = dateTime.Second;

        var hourString = hour.ToString().PadLeft(totalWidth: 2, paddingChar: '0');
        var minuteString = minute.ToString().PadLeft(totalWidth: 2, paddingChar: '0');
        var secondString = second.ToString().PadLeft(totalWidth: 2, paddingChar: '0');

        var result = $"{date} - {hourString}:{minuteString}:{secondString}";

        return result;
    }

    public static DateTime NowPersian(this DateTime dateTime)
    {
        var day = _persianCalendar.GetDayOfMonth(time: dateTime);
        var month = _persianCalendar.GetMonth(time: dateTime);
        var year = _persianCalendar.GetYear(time: dateTime);
        var hour = _persianCalendar.GetHour(time: dateTime);
        var min = _persianCalendar.GetMinute(time: dateTime);
        var sec = _persianCalendar.GetSecond(time: dateTime);

        var result = new DateTime(year, month, day, hour, min, sec);

        return result;
    }

    public static DateTime Now
    {
        get
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var currentUICulture = Thread.CurrentThread.CurrentUICulture;
            var englishCulture = new CultureInfo(name: "en-US");

            Thread.CurrentThread.CurrentCulture = englishCulture;
            Thread.CurrentThread.CurrentUICulture = englishCulture;

            var result = DateTime.Now;

            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentUICulture;

            return result;
        }
    }

    public static string FixText(string text)
    {
        if (string.IsNullOrWhiteSpace(value: text))
        {
            return null;
        }

        text = text.Trim();

        while (text.Contains(value: "  "))
        {
            text = text.Replace(oldValue: "  ", newValue: " ");
        }

        return text;
    }
}
