using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public abstract class ShopCard : MonoBehaviour
    {
        [SerializeField] protected ShopProductNames _shopProductNames;
        [Space(20)]
        [SerializeField] protected Image _productIcon;

        [SerializeField] protected Text _signatureField;
        [SerializeField] protected Text _buttonField;
        [SerializeField] protected Text _priceField;

        protected Action<ShopProductNames> _onClickPurchaseHandler;



        public abstract void Init(AssetProduct assetProduct, Action<ShopProductNames> purchaseHandler);


        public virtual void OnPurchaseButtonPressed()
        {
            AppMetricaManager.SendProductClick(_shopProductNames.ToString());
            _onClickPurchaseHandler?.Invoke(_shopProductNames);
        }
    }
}