using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class ResourcePresenterBase : MonoBehaviour
{
    /// <summary>
    /// Категория которая будет открыта при нажатии на презентер валюты
    /// </summary>
    [SerializeField] protected ShopCategoryType _shopCategoryType;

    [SerializeField] protected Text _walletField;


    
    protected virtual void ShowAmount(int count)
    {
        _walletField.text = count.ToString();
    }


    public void OnOpenShop()
    {
        if (ScenesManager.GetActiveScene() == "Shop")
            return;

        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        DataExchangeBetweenScenes.SetData(new ContainerData(ExchangeDataType.Shop_Category, _shopCategoryType));
        Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Shop");
    }
}
