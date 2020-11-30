using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AlertPopup : Popup
{
    [Header("Buttons")]
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _confirmButton;
    [Space(10), Header("Texts")]
    [SerializeField] private Text _headerField;
    [SerializeField] private Text _exceptionField;
    [Space(15)]
    [SerializeField] private List<AlertData> _alertDatas = new List<AlertData>();

    private AlertType _exceptionType;



    /// <summary>
    /// Инициализация ошибки
    /// </summary>
    /// <param name="exceptionType">Тип ошибки</param>
    /// <param name="addingMessage">Дополнительное сообщение, которое необходимо вывести</param>
    public void Init(AlertType exceptionType, string addingMessage = "")
    {
        _exceptionType = exceptionType;

        AlertData alertData = _alertDatas.Find(data => data.ExceptionType == exceptionType);

        _headerField.text = alertData.HeaderText;
        _exceptionField.text = $"{alertData.DescriptionText}\n{ addingMessage}";

        _shopButton.gameObject.SetActive(alertData.ShopButtonEnabled);
        _confirmButton.gameObject.SetActive(alertData.ConfirmButtonEnabled);
    }


    public void OnOpenShop()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        ShopCategoryType targetCategory = _exceptionType == AlertType.Lack_Chips ? ShopCategoryType.Chips : ShopCategoryType.Energy;
        DataExchangeBetweenScenes.SetData(new ContainerData(ExchangeDataType.Shop_Category, targetCategory));
        Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Shop");
    }
}

[System.Serializable]
public class AlertData
{
    public AlertType ExceptionType;
    public string HeaderText;
    public string DescriptionText;
    [Space(10)]
    public bool ShopButtonEnabled;
    public bool ConfirmButtonEnabled;
}