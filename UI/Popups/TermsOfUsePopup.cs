using Aledice.Extensions;
using UnityEngine;

public class TermsOfUsePopup : Popup
{
    public void OnPrivacyPolicyButtonPressed()
    {
        Application.OpenURL(Mediator.Instance.GameConfig.PrivacyPolicyURL);
    }


    public void OnTermsOfUseButtonPressed()
    {
        Application.OpenURL(Mediator.Instance.GameConfig.TermsOfUseURL);
    }


    public void OnAcceptButtonPressed()
    {
        PlayerPrefsAdvanced.SetBool("TermsOfUse", true);
        Close();
    }
}
