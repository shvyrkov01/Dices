public class DrawPopup : CompletedGamePopup
{
    protected override void ShowChangeChips(int chips)
    {
        _chipsField.text = $"{chips}";
    }
}
