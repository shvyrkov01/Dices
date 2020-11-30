using UnityEngine.Events;

public class ShopCategoryButton : HandlerButtonByEnumType<ShopCategoryType>
{
    public OnClickHandlerByType OnClickHandler = new OnClickHandlerByType();



    protected override void OnClickHandle()
    {
        OnClickHandler?.Invoke(_enumType);
    }


    [System.Serializable] public class OnClickHandlerByType : UnityEvent<ShopCategoryType> { }
}
