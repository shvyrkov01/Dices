using UnityEngine;
using Shop;
using System.Collections;

public class ShopScene : MonoBehaviour
{
    [SerializeField] private ShopCategorysPopup _shopCategorysPopup;
    [SerializeField] private ShopProductsPopup _shopProductsPopup;
    [SerializeField] private ShopProductsPresenter _shopProductsPresenter;



    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.02f);
        CheckTargetCategory();
    }


    public void OnExitToMenuButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
        Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Home");
    }


    public void OnBackToCategorysButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
        ChangeShowPopupsState(_shopCategorysPopup);
    }


    public void OnSpecialOfferButtonPressed(OfferData offerData)
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        if (offerData.SpecialOfferType == SpecialOfferType.Subscription)
            OnSubscriptionPopupOpen();
        else
        {
            Mediator.Instance.ShopIAPManager.BuyProduct(offerData.ShopProductName);
        }
    }


    public void OnCategoryButtonPressed(ShopCategoryType shopCategoryType)
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
        ChangeCategory(shopCategoryType);
    }


    private void ChangeCategory(ShopCategoryType shopCategoryType)
    {
        ChangeShowPopupsState(_shopProductsPopup);
        _shopProductsPresenter.Init(shopCategoryType);
    }


    private void ChangeShowPopupsState(Popup needPopup)
    {
        bool isNeedCategorysPopup = needPopup is ShopCategorysPopup;

        _shopCategorysPopup.gameObject.SetActive(isNeedCategorysPopup);
        _shopProductsPopup.gameObject.SetActive(!isNeedCategorysPopup);
    }


    /// <summary>
    /// Проверяет, сделан ли переход в магазин на определенную категорию товаров
    /// </summary>
    private void CheckTargetCategory()
    {
        var needShopCategory = DataExchangeBetweenScenes.TryGetDataByKey(ExchangeDataType.Shop_Category);

        if (ReferenceEquals(needShopCategory, null)) return;

        ChangeCategory((ShopCategoryType)needShopCategory);
    }


    private void OnSubscriptionPopupOpen()
    {
        Mediator.Instance.PopupsManager.CreatePopup<SubscriptionPopup>();
    }
}
