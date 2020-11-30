using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class SpecialOfferButton : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField] private ShopProductNames _shopProductName;

    [SerializeField] private float _changeOfferCooldown = 3f;

    [Space(20)]

    [SerializeField] private GameObject _productContainer;
    [SerializeField] private GameObject _subscriptionContainer;

    [SerializeField] private Image _iconField;
    [SerializeField] private Image _addingValuePanel;

    [SerializeField] private Text _oldPriceField;
    [SerializeField] private Text _newPriceField;

    [SerializeField] private Text _addingValueField;

    [Space(10)]
    [SerializeField] private SpecialOfferButtonHandler _specialOfferButtonHandler = new SpecialOfferButtonHandler();

    private bool _isInteractable = true;

    private AssetSpecialOffers _assetSpecialOffers;

    private OfferData _currentOfferData;




    private void Awake()
    {
        _assetSpecialOffers = Resources.Load<AssetSpecialOffers>("Assets/Shop/SpecialOffers/SpecialOffers");

        GetComponent<Button>().onClick.AddListener(() => _specialOfferButtonHandler?.Invoke(_currentOfferData));
    }


    private void OnEnable() => StartCoroutine(ChangeSpecialOfferTimer());


    private void OnDisable() => StopAllCoroutines();


    private void ApplyNewOffer()
    {
        bool isProduct = _currentOfferData.SpecialOfferType == SpecialOfferType.Product;

        _shopProductName = _currentOfferData.ShopProductName;

        _productContainer.SetActive(isProduct);
        _subscriptionContainer.SetActive(!isProduct);

        _oldPriceField.text = $"${_currentOfferData.OldPrice}";
        _newPriceField.text = $"${_currentOfferData.NewPrice}";

        if (isProduct)
            ShowSpecialOfferProduct();
    }


    private void ShowSpecialOfferProduct()
    {
        _iconField.sprite = _currentOfferData.Icon;
        _addingValueField.text = _currentOfferData.AddingValue.ToString();
    }


    private IEnumerator ChangeSpecialOfferTimer()
    {
        while(true)
        {
            foreach (OfferData offerData in _assetSpecialOffers.OfferDatas)
            {
                if (offerData.SpecialOfferType == SpecialOfferType.Subscription && Mediator.Instance.CustomStatisticsManager.IsSubscriptionPurchased)
                    continue;

                _currentOfferData = offerData;
                ApplyNewOffer();
                yield return new WaitForSeconds(_changeOfferCooldown);
            }
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (_isInteractable == false)
            return;

        ApplySwipe(eventData.delta.x > 0 ? SwipeSide.Right : SwipeSide.Left);
        _isInteractable = false;
    }


    private void ApplySwipe(SwipeSide swipeSide)
    {
        StopAllCoroutines();

        int offerDataId = _assetSpecialOffers.OfferDatas.FindIndex(data => data == _currentOfferData);
        
        if (swipeSide == SwipeSide.Right)
        {
            if (offerDataId == _assetSpecialOffers.OfferDatas.Count - 1)
                offerDataId = 0;
            else
                offerDataId++;
        }
        else
        {
            if (offerDataId == 0)
                offerDataId = _assetSpecialOffers.OfferDatas.Count - 1;
            else
                offerDataId--;
        }

        _currentOfferData = _assetSpecialOffers.OfferDatas[offerDataId];

        ApplyNewOffer();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isInteractable == true)
            FindObjectOfType<ShopScene>().OnSpecialOfferButtonPressed(_currentOfferData);
        else
            _isInteractable = true;
    }


    /// <summary>
    /// Обработчик клика по кнопке специального предложения
    /// </summary>
    [System.Serializable]
    public class SpecialOfferButtonHandler : UnityEvent<OfferData> { }
}
