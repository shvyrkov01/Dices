using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class HandlerButtonByEnumType<T> : MonoBehaviour
{
    [SerializeField] protected T _enumType;



    protected virtual void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            OnClickHandle();
        });
    }


    protected abstract void OnClickHandle();
}
