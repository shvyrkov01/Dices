using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Lean.Localization;

public abstract class CompletedGamePopup : Popup
{
    [SerializeField] private float _smoothForce = 100f;

    [SerializeField] protected Text _chipsField;
    [SerializeField] protected Text _playAgainField;

    [SerializeField] protected LeanToken _chipsCountToken;

    [Space(10)]

    [SerializeField] private Button _playAgainButton;
    [SerializeField] private Button _newRateButton;

    private GameMode _gameMode;

    



    private void Start()
    {
        _gameMode = FindObjectOfType<GameMode>();
        _smoothForce = GameplayManager.SettedRate;
    }


    protected override void OnEnable()
    {
        base.OnEnable();
    }


    protected override void OnDisable()
    {
        base.OnDisable();
    }


    public void ShowData(int chips, bool isEnabledPlayAgainButton = true, bool isEnabledNewRate = true)
    {
        _chipsCountToken.Value = GameplayManager.SettedRate.ToString();

        _playAgainButton.gameObject.SetActive(isEnabledPlayAgainButton);
        _newRateButton.gameObject.SetActive(isEnabledNewRate);

        StartCoroutine(ChangeChipsAnimation(chips));
    }


    public void OnExitToMenuButton()
    {
        Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Modes");
    }


    public void OnPlayAgainButtonPressed()
    {
        _gameMode.PlayAgain(false);

        Close();
    }


    public void OnNewRateButtonPressed()
    {
        _gameMode.PlayAgain(true);

        Close();
    }


    protected abstract void ShowChangeChips(int chips);


    private IEnumerator ChangeChipsAnimation(int countChips)
    {
        int currentShowedChips = 0;

        while (currentShowedChips != countChips)
        {
            yield return new WaitForEndOfFrame();

            currentShowedChips = (int)Mathf.MoveTowards(currentShowedChips, countChips, _smoothForce * 0.5f * Time.fixedDeltaTime);

            ShowChangeChips(currentShowedChips);
        }
    }
}
