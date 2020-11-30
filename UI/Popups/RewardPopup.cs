using UnityEngine;
using UnityEngine.UI;
using Extensions.Mathf;
using System.Collections.Generic;
using Lean.Localization;

public class RewardPopup : Popup
{
    private int _chipsReward;

    [SerializeField] private Text _chipsCountField;

    [Space(10)]

    [SerializeField] private LeanLocalizedText _headerLocalizedText;
    [SerializeField] private LeanLocalizedText _descriptionLocalizedText;

    [Space(10)]


    [SerializeField] private List<RewardData> _rewardDatas = new List<RewardData>();



    public void Init(RewardType rewardType = RewardType.Daily, int chipsCountReward = 0)
    {
        ShowRewardText(rewardType);
        
        if(chipsCountReward == 0)
        {
            RangeValueInt chipsRewardRange = Mediator.Instance.GameConfig.RangeChipValue;
            _chipsReward = MathfExtensions.RoundTenDigit(Random.Range(chipsRewardRange.Min, chipsRewardRange.Max));
        }
        else
        {
            _chipsReward = chipsCountReward;
        }
        
        _chipsCountField.text = _chipsReward.ToString();
    }


    private void ShowRewardText(RewardType rewardType)
    {
        RewardData rewardData = _rewardDatas.Find(data => data.RewardType == rewardType);

        if (ReferenceEquals(rewardData, null))
            return;

        _headerLocalizedText.TranslationName = rewardData.HeaderLocalizationKey;
        _descriptionLocalizedText.TranslationName = rewardData.DescriptionLocalizationKey;
    }


    public override void Close()
    {
        Mediator.Instance.ResourcesStorage.Chips.AddResources(_chipsReward);

        base.Close();
    }
}
