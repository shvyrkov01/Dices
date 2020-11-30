using UnityEngine.Events;

public class PausePopup : Popup
{
    public UnityEvent OnExit = new UnityEvent();



    public void OnContinueButtonPressed()
    {
        Close();
    }


    public void OnRestartButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        Mediator.Instance.PopupsManager.CreatePopup<ConfirmationPopup>().Init(() =>
        {
            OnExit?.Invoke();
            OnExit.RemoveAllListeners();
        
            Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Game");
        });
    }


    public void OnExitToMenuButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        Mediator.Instance.PopupsManager.CreatePopup<ConfirmationPopup>().Init(() =>
        {
            OnExit?.Invoke();
            OnExit.RemoveAllListeners();

            Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Modes");
        });
    }
}
