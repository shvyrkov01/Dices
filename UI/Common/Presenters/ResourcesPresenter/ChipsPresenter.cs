public class ChipsPresenter : ResourcePresenterBase
{
    protected void Start()
    {
        if (!ReferenceEquals(Mediator.Instance.ResourcesStorage.Chips, null))
            ShowAmount(Mediator.Instance.ResourcesStorage.Chips.Count);
    }


    private void OnEnable()
    {
        if (!ReferenceEquals(Mediator.Instance.ResourcesStorage.Chips, null))
            Mediator.Instance.ResourcesStorage.Chips.OnChangedResourcesCount.AddListener(ShowAmount);
    }


    protected void OnDisable()
    {
        if (!ReferenceEquals(Mediator.Instance.ResourcesStorage.Chips, null))
            Mediator.Instance.ResourcesStorage.Chips.OnChangedResourcesCount.RemoveListener(ShowAmount);
    }
}
