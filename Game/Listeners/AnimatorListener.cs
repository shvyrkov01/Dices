using UnityEngine;
using UnityEngine.Events;

public class AnimatorListener : MonoBehaviour
{
    public UnityEvent OnClipEnded = new UnityEvent();

    private bool _isOpened;



    public void OnClick()
    {
        if (_isOpened == false)
        {
            _isOpened = true;

            GetComponent<Animator>().SetTrigger("Opened");
            return;
        }

        OnClipEnded?.Invoke();
    }


    public void OnClipEnd()
    {
        OnClipEnded?.Invoke();
    }
}
