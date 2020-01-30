public interface IBoardModel {
    int Rows { get;}
    int Cols { get;}
    TileType this[int row, int col] { get;}
    bool SetTileType (int row, int col, TileType tileType);
    bool CheckWin(UnityEngine.Vector2Int tileClicked, out TileType? win);
    void ClearBoard();
}