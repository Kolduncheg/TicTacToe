using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas)), DisallowMultipleComponent]
class BoardView : MonoBehaviour, IBoardView
{
    public GridLayoutGroup gridLayout;
    public TileView tilePrefab;

    TileView[,] tiles;

    public Sprite cross;
    public Color crossColor;
    public Sprite circle;
    public Color circleColor;

    public event Action<ITileView> OnTileClick;

    public void Init(int rows, int cols)
    {
        tiles = new TileView[rows, cols];

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = cols;

        transform.GetComponent<RectTransform>().sizeDelta = new Vector2Int(cols, rows);

        DestroyAllChildren(gameObject);

        OnTileClick = null;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                TileView tile = Instantiate(tilePrefab);
                tile.transform.SetParent(gridLayout.transform);
                tile.transform.SetSiblingIndex(col + row * cols);
                tile.Init(row, col);

                tile.OnClick += OnTileClickedHandler;

                tiles[row, col] = tile;
            }
        }
    }

    private void DestroyAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            TileView tile = child.gameObject.GetComponent<TileView>();
            tile.OnClick -= OnTileClickedHandler;
            Destroy(child.gameObject);
        }
    }

    void OnTileClickedHandler(ITileView tile)
    {
        OnTileClick?.Invoke(tile);
    }

    public void RefreshBoard(IBoardModel model)
    {
        int rows = model.Rows;
        int cols = model.Cols;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                UpdateTile(row, col, model[row, col]);
            }
        }
    }

    public void UpdateTile(int row, int col, TileType tileType)
    {
        tiles[row, col].UpdateTile( tileType == TileType.Circle ? circle : (tileType == TileType.Cross ? cross : null) , 
                                         tileType == TileType.Circle ? Color.red : (tileType == TileType.Cross ? Color.blue : Color.clear),
                                         tileType == TileType.Circle ? false : (tileType == TileType.Cross ? false : true)
                                        );
    }
}