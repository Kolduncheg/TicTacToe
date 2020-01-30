using UnityEngine;

public class Player : IPlayer
{
    public TileType Type { get; protected set; }
    
    public event System.Action<Vector2Int> OnTurnEnd;
    
    protected GameBoardController board;

    public Player(GameBoardController board, TileType playerType)
    {
        this.board = board;
        PlayerType(playerType);
    }

    public void PlayerType(TileType playerType)
    {
        if (playerType == TileType.Empty)
        {
            Type = TileType.Cross;
            return;
        }

        Type = playerType;
    }

    public void StartTurn()
    {
        var input = board.GetView;
        input.OnTileClick += OnTileClicked;
    }

    void OnTileClicked(ITileView tile)
    {
        if (board[tile.Row, tile.Col] != TileType.Empty)
            return;

        var input = board.GetView;
        input.OnTileClick -= OnTileClicked;

        OnMoveCompleted(new Vector2Int(tile.Row, tile.Col));
    }

    public void OnMoveCompleted(Vector2Int playerMove)
    {
        OnTurnEnd?.Invoke(playerMove);
    }
}