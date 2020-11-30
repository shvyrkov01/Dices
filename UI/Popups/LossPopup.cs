public class LossPopup : CompletedGamePopup
{
    protected override void ShowChangeChips(int chips)
    {
        _chipsField.text = chips > 0 ? $"-{chips}" : chips.ToString();
    }
}
