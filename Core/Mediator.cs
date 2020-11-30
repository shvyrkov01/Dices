using UnityEngine;

[RequireComponent(typeof(PopupsManager))]
[RequireComponent(typeof(CustomStatisticsManager))]
public class Mediator : MonoBehaviour
{
    public static Mediator Instance;

    public PopupsManager PopupsManager { get; private set; }
    public CustomStatisticsManager CustomStatisticsManager { get; private set; }
    public ResourcesStorage ResourcesStorage { get; private set; }
    public SoundManager SoundManager { get; private set; }
    public ShopIAPManager ShopIAPManager { get; private set; }
    public GameConfig GameConfig { get; private set; }
    public DatabaseExchanger DatabaseExchanger { get; private set; }



    private void Awake()
    {
        #region Singleton
        DontDestroyOnLoad(gameObject);

        if (ReferenceEquals(Instance, null))
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion

        InitComponents();
    }


    private void InitComponents()
    {
        PopupsManager = GetComponent<PopupsManager>();
        CustomStatisticsManager = GetComponent<CustomStatisticsManager>();

        ResourcesStorage = GetComponentInChildren<ResourcesStorage>();
        SoundManager = GetComponentInChildren<SoundManager>();
        ShopIAPManager = GetComponentInChildren<ShopIAPManager>();
        DatabaseExchanger = GetComponentInChildren<DatabaseExchanger>();

        GameConfig = Resources.Load<GameConfig>("GameConfig");
    }


    private void OnDisable()
    {
        if (Instance != this) return;
            AppMetricaManager.SendUserData(CustomStatisticsManager.UserData);
    }

}
