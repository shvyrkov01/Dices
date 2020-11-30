using System;

public class DateFormater
{
    /// <summary>
    /// Получить дату с форматом для базы данных
    /// </summary>
    /// <returns></returns>
    public static string GetDateNowFormatDatabase()
    {
        DateTime dateTime = DateTime.Now;
        return $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}";
    }
}
