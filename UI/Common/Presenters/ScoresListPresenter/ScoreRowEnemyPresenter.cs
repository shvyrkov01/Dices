using UnityEngine;
using UnityEngine.UI;

public class ScoreRowEnemyPresenter : ScoreRowPresenter
{

    public override void Init(int scoreValue, bool isShowedUsername)
    {
        base.Init(scoreValue, isShowedUsername);

        if (isShowedUsername && GameplayManager.EnemyUsername != string.Empty)
            _entityUsernameField.text = GameplayManager.EnemyUsername;
    }
}
