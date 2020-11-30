using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;

public class ShopIAPManager : MonoBehaviour
{
    private Action _productBoughtCallback;



    private void Start()
    {
        IAPManager.Instance.InitializeIAPManager(InitComplete);
    }


    public void BuyProduct(ShopProductNames shopProductName, Action productBoughtCallback = null)
    {
        if (Mediator.Instance.GameConfig.IsDebug)
        {
            BuyProductDebug();
            return;
        }

        _productBoughtCallback = productBoughtCallback;
        IAPManager.Instance.BuyProduct(shopProductName, ProductBought);
    }


    public void TryRestorePurchases()
    {
        IAPManager.Instance.RestorePurchases(ProductRestoredCallback);
    }


    private void InitComplete(IAPOperationStatus status, string errorMessage, List<StoreProduct> storeProducts)
    {
        if(status == IAPOperationStatus.Success)
        {
            for (int i = 0; i < storeProducts.Count; i++)
            {
                if(Mediator.Instance.GameConfig.IsDebug)
                    Debug.Log($"Init product: {storeProducts[i].productName} && Active: {CheckProductActive(storeProducts[i])}");

                if(storeProducts[i].productName == ShopProductNames.Subscription1.ToString())
                    if(CheckProductActive(storeProducts[i]))
                        Mediator.Instance.CustomStatisticsManager.IsSubscriptionPurchased = true;
                
                if(storeProducts[i].productName == ShopProductNames.DiceBR.ToString())
                    if (CheckProductActive(storeProducts[i]))
                        Mediator.Instance.CustomStatisticsManager.UserData.ApplyPurchasedDice("BR");

                if (storeProducts[i].productName == ShopProductNames.DiceBW.ToString())
                    if (CheckProductActive(storeProducts[i]))
                        Mediator.Instance.CustomStatisticsManager.UserData.ApplyPurchasedDice("BW");
            }
        }

        CheckSubscription();
    }


    private void BuyProductDebug()
    {
        Mediator.Instance.ResourcesStorage.Chips.AddResources(1000);
        Mediator.Instance.ResourcesStorage.Energy.AddResources(10);
    }


    private bool CheckProductActive(StoreProduct storeProduct)
    {
        return storeProduct.active;
    }
    

    private void CheckSubscription()
    {
        SubscriptionInfo subscriptionInfo = IAPManager.Instance.GetSubscriptionInfo(ShopProductNames.Subscription1);

        if(subscriptionInfo == null)
        {
            Debug.Log("SubscriptionInfo равняется NULL!");
            return;
        }

        if (subscriptionInfo.getExpireDate() < System.DateTime.Now)
            Mediator.Instance.CustomStatisticsManager.IsSubscriptionPurchased = false;
    }


    private void ProductBought(IAPOperationStatus status, string errorMessage, StoreProduct storeProduct)
    {
        if (status == IAPOperationStatus.Success)
        {
            if (storeProduct.productName == ShopProductNames.Chips100.ToString())
                Mediator.Instance.ResourcesStorage.Chips.AddResources(100);
            else if (storeProduct.productName == ShopProductNames.Chips1000.ToString())
                Mediator.Instance.ResourcesStorage.Chips.AddResources(1000);
            else if (storeProduct.productName == ShopProductNames.Chips10000.ToString())
                Mediator.Instance.ResourcesStorage.Chips.AddResources(10000);
            else if (storeProduct.productName == ShopProductNames.Chips1000_SpecialOffer.ToString())
                Mediator.Instance.ResourcesStorage.Chips.AddResources(1000);
            else if (storeProduct.productName == ShopProductNames.Chips200.ToString())
                Mediator.Instance.ResourcesStorage.Chips.AddResources(200);
            else if (storeProduct.productName == ShopProductNames.Chips2000.ToString())
                Mediator.Instance.ResourcesStorage.Chips.AddResources(2000);
            else if (storeProduct.productName == ShopProductNames.Chips300.ToString())
                Mediator.Instance.ResourcesStorage.Chips.AddResources(300);
            else if (storeProduct.productName == ShopProductNames.Chips400.ToString())
                Mediator.Instance.ResourcesStorage.Chips.AddResources(400);
            else if (storeProduct.productName == ShopProductNames.Chips500.ToString())
                Mediator.Instance.ResourcesStorage.Chips.AddResources(500);
            else if (storeProduct.productName == ShopProductNames.Chips5000.ToString())
                Mediator.Instance.ResourcesStorage.Chips.AddResources(5000);
            else if (storeProduct.productName == ShopProductNames.DiceBR.ToString())
                Mediator.Instance.CustomStatisticsManager.UserData.ApplyPurchasedDice("BR");
            else if (storeProduct.productName == ShopProductNames.DiceBW.ToString())
                Mediator.Instance.CustomStatisticsManager.UserData.ApplyPurchasedDice("BW");
            else if (storeProduct.productName == ShopProductNames.Energy1.ToString())
                Mediator.Instance.ResourcesStorage.Energy.AddResources(1);
            else if (storeProduct.productName == ShopProductNames.Energy10.ToString())
                Mediator.Instance.ResourcesStorage.Energy.AddResources(10);
            else if (storeProduct.productName == ShopProductNames.Energy5.ToString())
                Mediator.Instance.ResourcesStorage.Energy.AddResources(5);
            else if (storeProduct.productName == ShopProductNames.Energy_Full_SpecialOffer.ToString())
                Mediator.Instance.ResourcesStorage.Energy.AddResources(10);
            else if (storeProduct.productName == ShopProductNames.Subscription1.ToString())
                Mediator.Instance.CustomStatisticsManager.IsSubscriptionPurchased = true;
            else if (storeProduct.productName == ShopProductNames.Surprise1.ToString())
                Mediator.Instance.ResourcesStorage.SurpriseBox.AddResources(1);

            SendPurchasedDataToDatabase(storeProduct);
        }
        else
            Debug.LogError(errorMessage);

        if (_productBoughtCallback != null)
            _productBoughtCallback?.Invoke();
    }


    private void SendPurchasedDataToDatabase(StoreProduct storeProduct)
    {
        Mediator.Instance.DatabaseExchanger.TrySubmitPurchase(storeProduct.productName, storeProduct.priceInRubles, true);
    }


    private void ProductRestoredCallback(IAPOperationStatus status, string errorMessage, StoreProduct storeProduct)
    {
        if(status == IAPOperationStatus.Success)
        {
            if (storeProduct.productName == ShopProductNames.Subscription1.ToString())
                if(CheckProductActive(storeProduct))
                    Mediator.Instance.CustomStatisticsManager.IsSubscriptionPurchased = true;

            if (storeProduct.productName == ShopProductNames.DiceBR.ToString())
                if (CheckProductActive(storeProduct))
                    Mediator.Instance.CustomStatisticsManager.UserData.ApplyPurchasedDice("BR");

            if (storeProduct.productName == ShopProductNames.DiceBW.ToString())
                if (CheckProductActive(storeProduct))
                    Mediator.Instance.CustomStatisticsManager.UserData.ApplyPurchasedDice("BW");
        }
    }
}
