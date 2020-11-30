using UnityEngine.SceneManagement;

public class ScenesManager 
{
    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }
}
