using UnityEngine;
using UnityEngine.UI;
using System;
using Lean.Localization;

namespace Shop
{
    public class ProductCardDices : ShopCard
    {
        public static event Action OnSelectedDice;

        [SerializeField] private Text _buttonText;
        [SerializeField] private Text _specificationsField;

        private bool _isPurchasedDice;

        private AssetProductDice _assetProductDice;



        public override void Init(AssetProduct assetProduct, Action<ShopProductNames> purchaseHandler)
        {
            _assetProductDice = (AssetProductDice)assetProduct;
            _onClickPurchaseHandler = purchaseHandler;

            DiceParameter diceParameter = Mediator.Instance.GameConfig.DiceParameters.Find(dice => dice.DiceID == ((AssetProductDice)assetProduct).DiceID);

            _isPurchasedDice = Mediator.Instance.CustomStatisticsManager.UserData.HasPurchasedDice(_assetProductDice.DiceID);

            _productIcon.sprite = assetProduct.Icon;
            _signatureField.text = assetProduct.Signature;
            _priceField.text = $"${assetProduct.Price}";

            ShowSpecificationData((AssetProductDice)assetProduct, diceParameter);

            DetectButtonSignature();
        }


        private void OnEnable()
        {
            ProductCardDices.OnSelectedDice += DetectButtonSignature;
        }


        private void OnDisable()
        {
            ProductCardDices.OnSelectedDice -= DetectButtonSignature;
        }


        private void DetectButtonSignature()
        {
            if (!Mediator.Instance.CustomStatisticsManager.UserData.HasPurchasedDice(_assetProductDice.DiceID)) return;

            if(PlayerPrefs.HasKey("SelectedDiceID"))
            {
                string selectedDiceID = PlayerPrefs.GetString("SelectedDiceID");

                if (_assetProductDice.DiceID == selectedDiceID)
                    _buttonText.text = LeanLocalization.GetTranslationText("Button/Selected");
                else
                    _buttonText.text = LeanLocalization.GetTranslationText("Button/Select");
            }


            _priceField.gameObject.SetActive(false);
        }


        private void SelectDice()
        {
            PlayerPrefs.SetString("SelectedDiceID", _assetProductDice.DiceID);
            OnSelectedDice?.Invoke();
        }


        private void ShowSpecificationData(AssetProductDice assetProductDice, DiceParameter diceParameter)
        {
            _specificationsField.text = string.Empty;

            if (diceParameter.AddingCoefficient == 0) return;

            _specificationsField.text = assetProductDice.Specifications + diceParameter.AddingCoefficient;
        }


        public override void OnPurchaseButtonPressed()
        {
            if (_isPurchasedDice == false)
                base.OnPurchaseButtonPressed();
            else
                SelectDice();
        }
    }
}