using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ScrollView : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public float Step => _step;

    [SerializeField] private ScrollAxis _scrollAxis = ScrollAxis.Horizontal;

    [Space(20)]
    
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private float _step;

    [SerializeField] private RangeValueFloat _scrollRange;

    [SerializeField] private RectTransform _containerRectTransform;

    private bool _isInteractable = true;

    private Vector2 _targetPosition;

    private Coroutine _swipingCoroutine;



    public void Init(int cardCount)
    {
        _scrollRange.Max = -cardCount * (_step - 1);
    }


    public void Init(float scrollRangeMax)
    {
        _scrollRange.Max = scrollRangeMax;
    }


    private void OnDisable()
    {
        _containerRectTransform.localPosition = new Vector2(0, _containerRectTransform.localPosition.y);
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (_isInteractable == false)
            return;

        _isInteractable = false;

        if (_scrollAxis == ScrollAxis.Horizontal)
            ApplySwipe(eventData.delta.x > 0 ? SwipeSide.Right : SwipeSide.Left);
        else
            ApplySwipe(eventData.delta.y > 0 ? SwipeSide.Up : SwipeSide.Down);
    }


    private void ApplySwipe(SwipeSide swipeSide)
    {
        if (!ReferenceEquals(_swipingCoroutine, null))
            return;

        _targetPosition = _containerRectTransform.localPosition;

        if (_scrollAxis == ScrollAxis.Horizontal)
        {
            if (swipeSide == SwipeSide.Left && _containerRectTransform.localPosition.x - Step >= _scrollRange.Max)
                _targetPosition.x -= _step;
            else if (swipeSide == SwipeSide.Right && _containerRectTransform.localPosition.x + Step <= _scrollRange.Min)
                _targetPosition.x += _step;
        }
        else
        {
            if (swipeSide == SwipeSide.Down && _containerRectTransform.localPosition.y - Step >= _scrollRange.Max)
                _targetPosition.y -= _step;
            else if (swipeSide == SwipeSide.Up && _containerRectTransform.localPosition.y + Step <= _scrollRange.Min)
                _targetPosition.y += _step;
        }

        _swipingCoroutine = StartCoroutine(Swiping());
    }


    private IEnumerator Swiping()
    {
        while (_containerRectTransform.localPosition.x != _targetPosition.x || _containerRectTransform.localPosition.y != _targetPosition.y)
        {
            _containerRectTransform.localPosition = Vector2.MoveTowards(_containerRectTransform.localPosition, _targetPosition, _scrollSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        _swipingCoroutine = null;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        _isInteractable = true;
    }
}
