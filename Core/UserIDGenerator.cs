using System;

public class UserIDGenerator
{
    public static string GetUserID()
    {
        string userID = string.Empty;

        Random random = new Random();

        for (int i = 0; i < 15; i++)
            userID += random.Next(0, 9).ToString();

        return userID;
    }
}
