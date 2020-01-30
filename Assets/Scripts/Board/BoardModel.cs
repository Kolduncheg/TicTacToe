using UnityEngine;

public class BoardModel : IBoardModel
{
    TileType[,] tiles;
    int emptyTiles;

    public int Rows { get; protected set; }
    public int Cols { get; protected set; }

    public TileType this[int row, int col] => tiles[row, col];

    public BoardModel(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;

        tiles = new TileType[Rows, Cols];

        emptyTiles = Rows * Cols;
    }

    public bool CheckWin(Vector2Int tileClicked, out TileType? win)
    {
        if (CheckWinCombination(tileClicked) == true){
            win = tiles[tileClicked.x, tileClicked.y];
            return true;
        };

        win = null;
        return emptyTiles == 0;
    }

    public bool CheckWinCombination(Vector2Int tileClicked) {

        TileType tileType = tiles[tileClicked.x, tileClicked.y];

        for (int x = 0; x < Rows; x++)
        {
            if (this[x, tileClicked.y] != tileType)
            {
                break;
            }

            if (x == Rows - 1)
                return true;
        }

        for (int y = 0; y < Cols; y++)
        {
            if (this[tileClicked.x, y] != tileType)
            {
                break;
            }

            if (y == Cols - 1) 
                return true;
               
        }

        if (tileClicked.x == tileClicked.y)
        {
            for (int i = 0; i < Cols; i++)
            {
                if (this[i,i] != tileType)
                    break;

                if (i == Cols - 1)
                    return true;
            }
        }

        if (tileClicked.x + tileClicked.y == Rows - 1)
        {
            for (int i = 0; i < Rows; i++)
            {
                if (this[i, (Rows - 1) - i] != tileType)
                    break;

                if (i == Rows - 1)
                    return true;
            }
        }

        return false;
    }

    public void ClearBoard()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Cols; col++)
            {
                tiles[row, col] = TileType.Empty;
            }
        }

        emptyTiles = Rows * Cols;
    }

    public bool SetTileType(int row, int col, TileType tileType)
    {
        if (tiles[row, col] != TileType.Empty)
            return false;

        emptyTiles --;
        tiles[row, col] = tileType;

        return true;
    }
}
