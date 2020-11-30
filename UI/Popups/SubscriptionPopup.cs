using UnityEngine;
using UnityEngine.UI;

public class SubscriptionPopup : Popup
{
    [SerializeField] private Button _closeButton;



    protected override void OnEnable()
    {
        AppMetricaManager.SendSubscriptionPopupOpen();
        StartCoroutine(ActionWithDelay(() => _closeButton.gameObject.SetActive(true), 2f));
    }


    public void OnPurchaseButtonPressed()
    {
        AppMetricaManager.SendSubscriptionButtonClick();
        Mediator.Instance.ShopIAPManager.BuyProduct(ShopProductNames.Subscription1);
    }
}
