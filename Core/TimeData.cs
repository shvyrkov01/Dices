using System;

[Serializable]
public class TimeData
{
    public int Day;
    public int Month;
    public int Year;

    public int Hour;
    public int Minute;
    public int Second;


    public TimeData(DateTime dateTime)
    {
        Day = dateTime.Day;
        Month = dateTime.Month;
        Year = dateTime.Year;

        Hour = dateTime.Hour;
        Minute = dateTime.Minute;
        Second = dateTime.Second;
    }


    public DateTime GetDateTime()
    {
        return new DateTime(Year, Month, Day, Hour, Minute, Second);
    }
}