using UnityEngine;

public class TargetFPS : MonoBehaviour
{
    private void Awake()
    {
        if (!Application.isMobilePlatform) return;

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
}