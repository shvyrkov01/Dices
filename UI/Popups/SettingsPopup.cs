using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : Popup
{
    [SerializeField] private Image _soundButtonIcon;
    [SerializeField] private Image _vibrationButtonIcon;

    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;

    [SerializeField] private Sprite _vibrationOn;
    [SerializeField] private Sprite _vibrationOff;



    private void Start()
    {
        ChangeButtonIcon(ButtonType.Sound);
        ChangeButtonIcon(ButtonType.Vibration);
    }


    public void OnSoundButtonPressed()
    {
        PlayerPrefs.SetInt("SoundEnabled", PlayerPrefs.GetInt("SoundEnabled", 1) == 1 ? 0 : 1);
        Mediator.Instance.SoundManager.CheckSoundEnabled();
        ChangeButtonIcon(ButtonType.Sound);

        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
            Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
    }


    public void OnVibrationButtonPressed()
    {
        PlayerPrefs.SetInt("VibrationEnabled", PlayerPrefs.GetInt("VibrationEnabled", 1) == 1 ? 0 : 1);
        ChangeButtonIcon(ButtonType.Vibration);

        if(PlayerPrefs.GetInt("VibrationEnabled") == 1)
            MMVibrationManager.Haptic(HapticTypes.MediumImpact, false, true, this);

    }


    public void OnTermsOfUseButtonPressed()
    {
        Application.OpenURL(Mediator.Instance.GameConfig.TermsOfUseURL);
    }


    private void ChangeButtonIcon(ButtonType buttonType)
    {
        if (buttonType == ButtonType.Sound)
            _soundButtonIcon.sprite = PlayerPrefs.GetInt("SoundEnabled", 1) == 1 ? _soundOn : _soundOff;
        else if (buttonType == ButtonType.Vibration)
            _vibrationButtonIcon.sprite = PlayerPrefs.GetInt("VibrationEnabled", 1) == 1 ? _vibrationOn : _vibrationOff;
    }


    private enum ButtonType
    {
        Sound,
        Vibration
    }
}
