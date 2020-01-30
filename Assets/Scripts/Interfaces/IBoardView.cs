public interface IBoardView
{
    void Init(int rows, int cols);
    void UpdateTile(int row, int col, TileType tileType);
    void RefreshBoard(IBoardModel model);
    event System.Action<ITileView> OnTileClick;
}