using UnityEngine;

[RequireComponent(typeof(BoneGenerator))]
public class BoneGeneratorInputListener : MonoBehaviour
{
    [SerializeField] private BoneGenerator _boneGenerator;

    private bool _isGameplayStarted;



    private void OnEnable()
    {
        InputManager.OnClick += OnClickHandler;
        InputManager.OnSwipe += OnSwipeHandler;

        GameplayManager.OnChangedGameplayState += OnChangeGameplayState;
    }


    private void OnDisable()
    {
        InputManager.OnClick -= OnClickHandler;
        InputManager.OnSwipe -= OnSwipeHandler;

        GameplayManager.OnChangedGameplayState -= OnChangeGameplayState;
    }


    private void OnClickHandler()
    {
        if (_isGameplayStarted == false) return;

        _boneGenerator.TryThrowBones();
    }


    private void OnSwipeHandler(Vector3 swipeDirection)
    {
        if (_isGameplayStarted == false) return;

        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            var targetBasket = swipeDirection.x < 0 ? 0 : 1;
            _boneGenerator.TryClearGameField(targetBasket);
        }
        else
        {
            if (swipeDirection.y > 0)
                _boneGenerator.TryThrowBones();
        }
    }


    private void OnChangeGameplayState(GameplayState gameplayState)
    {
        _isGameplayStarted = gameplayState == GameplayState.Started;
    }
}
