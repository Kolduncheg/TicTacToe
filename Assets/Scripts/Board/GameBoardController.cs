using System;
using UnityEngine;

public class GameBoardController
{
    IBoardModel model;
    IBoardView view;

    public int Rows => model.Rows;
    public int Cols => model.Cols;

    public TileType this[int row, int col] => model[row, col];

    public IBoardView GetView => view;

    public GameBoardController(IBoardView view, IBoardModel model)
    {
        this.model = model;
        
        this.view = view;
        view.Init(model.Rows, model.Cols);
        view.RefreshBoard(model);
    }

    public void SetTileState(int row, int col, TileType tileType)
    {
        if (model.SetTileType(row, col, tileType))
        {
            view.UpdateTile(row, col, tileType);
        }
    }

    public bool CheckGameOver(Vector2Int tile, out TileType? win)
    {
        return model.CheckWin(tile, out win);
    }

    public void Clear()
    {
        model.ClearBoard();
        view.RefreshBoard(model);
    }
}