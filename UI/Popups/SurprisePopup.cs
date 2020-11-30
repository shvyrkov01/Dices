using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Extensions.Mathf;

public class SurprisePopup : Popup, IPointerClickHandler
{
    [SerializeField] private Image _surpriseImage;

    [Space(20)]

    [SerializeField] private Sprite _chipsSprite;
    [SerializeField] private Sprite _energySprite;

    /// <summary>
    /// Подарок получен
    /// </summary>
    private bool _isGettedSurprise;



    public void OnPointerClick(PointerEventData eventData)
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        if (_isGettedSurprise)
            Close();
    }


    public void ShowSurprise()
    {
        if (_isGettedSurprise)
        {
            Close();
            return;
        }

        _surpriseImage.gameObject.SetActive(true);
        IdentifyGift();
    }


    private void IdentifyGift()
    {
        ResourceType resourceType = default;

        int addingValue = 0;

        if (Random.Range(0f, 1f) < Mediator.Instance.GameConfig.EnergyDropChance)
            resourceType = ResourceType.Energy;

        _surpriseImage.sprite = resourceType == ResourceType.Chips ? _chipsSprite : _energySprite;
        addingValue = resourceType == ResourceType.Chips ? MathfExtensions.RoundTenDigit(Mediator.Instance.GameConfig.RangeChipValue.GetRandomValue()) : Mediator.Instance.GameConfig.RangeEnergyValue.GetRandomValue();
        _surpriseImage.GetComponentInChildren<Text>().text = addingValue.ToString();

        if (resourceType == ResourceType.Chips)
            Mediator.Instance.ResourcesStorage.Chips.AddResources(addingValue);
        else if (resourceType == ResourceType.Energy)
            Mediator.Instance.ResourcesStorage.Energy.AddResources(addingValue);

        _isGettedSurprise = true;
    }
}
