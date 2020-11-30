using UnityEngine;
using System.Collections;

public class BoneDetector : MonoBehaviour
{
    public Bone GetSettedBone()
    {
        DiceParameter diceParameter = Mediator.Instance.GameConfig.DiceParameters.Find(dice => dice.DiceID == PlayerPrefs.GetString("SelectedDiceID", "WB"));
        BoneModel boneModel = diceParameter.BonePrefab.GetComponentInChildren<BoneModel>();

        boneModel.BodyMaterial.color = diceParameter.BodyColor;
        boneModel.PointsMaterial.color = diceParameter.PointsColor;

        return diceParameter.BonePrefab;
    }
}
