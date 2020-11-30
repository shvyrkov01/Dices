using UnityEngine;
using System.Collections;

public class ScoreRowPlayerPresenter : ScoreRowPresenter
{
    public override void Init(int scoreValue, bool isShowedUsername)
    {
        base.Init(scoreValue, isShowedUsername);

        if (isShowedUsername)
            _entityUsernameField.text = Mediator.Instance.CustomStatisticsManager.UserData.Username;
    }
}
