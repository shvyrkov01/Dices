using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler
{
    public static event Action<Vector3> OnSwipe;
    public static event Action OnClick;

    private Vector3 _lowPassValue;
    private Vector3 _acceleration;
    private Vector3 _deltaAcceleration;

    private Vector2 _startSwipePosition;

    private float _lowPassFilterFactor = 0.01f;

    private bool _isDrag;
    private bool _isCompleteSwipe;
    private bool _isGyroscopeEnabled;


    




    private void Start()
    {
        _isGyroscopeEnabled = SystemInfo.supportsGyroscope;
    }


    private void Update()
    {
        if (_isGyroscopeEnabled == false)
            return;

        _acceleration = Input.acceleration;
        _lowPassValue = Vector3.Lerp(_lowPassValue, _acceleration, _lowPassFilterFactor);
        _deltaAcceleration = _acceleration - _lowPassValue;

        if (_deltaAcceleration.sqrMagnitude >= 2f)
        {
            OnPointerClick(null);
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;
        _startSwipePosition = eventData.position;
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (_isCompleteSwipe == true) return;

        Vector2 difference = (eventData.position - _startSwipePosition);
        Vector2 directionNormalize = difference.normalized;

        if (difference.magnitude < 8) return;

        Vector3 swipeDirection;

        if (Mathf.Abs(directionNormalize.x) > Mathf.Abs(directionNormalize.y))
        {
            swipeDirection = directionNormalize.x > 0 ? transform.right : -transform.right;
        }
        else
        {
            swipeDirection = directionNormalize.y > 0 ? Vector3.up : -Vector3.up;
        }

        OnSwipe?.Invoke(swipeDirection);
        _isCompleteSwipe = true;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        _isCompleteSwipe = false;

        if (_isDrag)
        {
            _isDrag = false;
            return;
        }

        OnClick?.Invoke();
    }
}
