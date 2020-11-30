using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    [Header("Modes")]
    [SerializeField] private CombinationAtRandomMode _combinationAtRandomMode;
    [SerializeField] private TwentyOneMode _twentyOneMode;
    [SerializeField] private ThrowMoreOfflineMode _throwMoreOfflineMode;
    [SerializeField] private ThrowMoreOnlineMode _throwMoreOnlineMode;

    [Header("Other"), Space(15)]
    [SerializeField] private Button _stopButton;

    private GameMode _gameModeInstance;



    private void Awake()
    {
        InitGameMode();
    }


    public void OnPauseButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
        Mediator.Instance.PopupsManager.CreatePopup<PausePopup>().OnExit.AddListener(() =>
        {
            _gameModeInstance.OnExitGameFromPausePopup();
        });
    }


    public void OnHelpButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
        Mediator.Instance.PopupsManager.CreatePopup<HelpPopup>();
    }


    /// <summary>
    /// Инициализирует выбранный игровой режим
    /// </summary>
    private void InitGameMode()
    {
        GameMode gameMode = null;

        switch(GameplayManager.SelectedGameModeType)
        {
            case GameModeType.BetAtRandom: gameMode = _combinationAtRandomMode; break;

            case GameModeType.Throw_Offline: gameMode = _throwMoreOfflineMode; break;

            case GameModeType.Throw_Online:
                gameMode = _throwMoreOnlineMode; 
                _stopButton.gameObject.SetActive(true);
            break;

            case GameModeType.TwentyOne: gameMode = _twentyOneMode; break;
        }

        if (ReferenceEquals(gameMode, null)) return;

        _gameModeInstance = Instantiate(gameMode);
        InitComponents(_gameModeInstance);
    }


    private void InitComponents(GameMode gameMode)
    {
        if(GameplayManager.SelectedGameModeType == GameModeType.Throw_Online)
            _stopButton.onClick.AddListener(() => ((ThrowMoreOnlineMode)gameMode).ApplyEndGame());
    }
}
