using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomStatisticsManager : MonoBehaviour
{
    public bool IsSubscriptionPurchased;
    public bool IsGameScene { get; private set; }
    

    public UserData UserData => _userData;
    [SerializeField] private UserData _userData;



    private void Awake()
    {
        Init();
    }


    private void Init()
    {
        _userData = DataLoader.LoadData<UserData>() ?? new UserData();
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnDestroy()
    {
        SaveUserData();
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        IsGameScene = scene.name == "Game";
        AppMetricaManager.SendOpenScene(scene.name);
    }


    private void SaveUserData()
    {
        DataLoader.SaveData(_userData);
    }
}
