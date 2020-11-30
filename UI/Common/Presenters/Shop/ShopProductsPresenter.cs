using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Lean.Localization;

namespace Shop
{
    public class ShopProductsPresenter : MonoBehaviour
    {
        [SerializeField] private Text _categoryField;

        [SerializeField] private ProductCardResoucres _productCardResourcesTemplate;
        [SerializeField] private ProductCardDices _productCardDicesTemplate;

        [SerializeField] private Transform _productCardsContainer;

        [SerializeField] private ScrollView _scrollView;

        private ShopCategoryType _shopCategoryType;

        private List<AssetProduct> _assetProducts;



        public void Init(ShopCategoryType shopCategoryType)
        {
            _shopCategoryType = shopCategoryType;

            string category = string.Empty;
            string pathToProducts = string.Empty;

            switch(shopCategoryType)
            {
                case ShopCategoryType.Chips: pathToProducts = "Chips"; category = LeanLocalization.GetTranslationText("Chips"); break;
                case ShopCategoryType.Energy: pathToProducts = "Energy"; category = LeanLocalization.GetTranslationText("Energy"); ; break;
                case ShopCategoryType.Dices: pathToProducts = "Dices"; category = LeanLocalization.GetTranslationText("Dices"); ; break;
            }

            print($"Shop category: {shopCategoryType}");

            _categoryField.text = category;
            _assetProducts = Resources.LoadAll<AssetProduct>($"Assets/Shop/Products/{pathToProducts}").ToList();
            _assetProducts = _assetProducts.OrderBy(product => product.Price).ToList();

            RenderProducts();
        }


        public void RenderProducts()
        {
            ClearContainer();

            ShopCard shopCard = GetShopCardTemplate();

            Vector2 spawnPosition = Vector2.zero;

            _assetProducts.ForEach(assetProduct =>
            {
                ShopCard productCard = Instantiate(shopCard, _productCardsContainer);
                productCard.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
                productCard.Init(assetProduct, (shopProductName) => Mediator.Instance.ShopIAPManager.BuyProduct(assetProduct.ShopProductName, RenderProducts));

                spawnPosition.x += _scrollView.Step;
            });

            _scrollView.Init(_assetProducts.Count);
        }


        private ShopCard GetShopCardTemplate()
        {
            if (_shopCategoryType == ShopCategoryType.Dices)
                return _productCardDicesTemplate;

            return _productCardResourcesTemplate;
        }


        private void ClearContainer()
        {
            foreach (Transform child in _productCardsContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

