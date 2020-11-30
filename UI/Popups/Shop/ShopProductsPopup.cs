public class ShopProductsPopup : Popup
{
    protected override void OnEnable()
    {
        OnOpen?.Invoke();
    }


    protected override void OnDisable()
    {
        base.OnDisable();
        OnClose?.Invoke();    
    }
}
