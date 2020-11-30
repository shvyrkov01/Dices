using UnityEngine;
using UnityEngine.UI;
using System;
using Lean.Localization;

namespace Shop
{
    public class ProductCardResoucres : ShopCard
    {
        [SerializeField] private Text _countField;



        public override void Init(AssetProduct assetProduct, Action<ShopProductNames> purchaseHandler)
        {
            _onClickPurchaseHandler = purchaseHandler;

            _productIcon.sprite = assetProduct.Icon;
            _signatureField.text = LeanLocalization.GetTranslationText(((AssetProductResource)assetProduct).AddingProduct.ToString());
            
            if(assetProduct.CurrencyType == CurrencyType.Real)
            {
                _priceField.text = $"${assetProduct.Price}";
            }


            if(assetProduct is IProductConsumable)
                _countField.text = ((IProductConsumable)assetProduct).AddingProduct.ToString();
        }
    }
}