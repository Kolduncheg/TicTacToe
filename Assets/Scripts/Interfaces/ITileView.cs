public interface ITileView
{
    int Row { get; }
    int Col { get; }
    event System.Action<ITileView> OnClick;
}