using UnityEngine;
using System;

class TurnManager
{
    public Action<Player> OnGameOver;
    
    Action<Vector2Int> _OnTurnComplete;
    
    GameBoardController gameBoard;
    
    Player[] players;
    Player currentPlayer => players[currentPlayerIdx];

    int currentPlayerIdx = UnityEngine.Random.Range(0,2);

    public TurnManager(GameBoardController board, params Player[] players)
    {
        gameBoard = board;
        this.players = players;
        _OnTurnComplete = OnTurnComplete;
    }

    public void Start()
    {
        for (int i = 0; i < players.Length; ++i)
            players[i].PlayerType(i == currentPlayerIdx ? TileType.Cross : TileType.Circle);

        StartNextTurn(null);
    }

    void StartNextTurn(Vector2Int? previousMove)
    {
        if (previousMove != null)
            currentPlayerIdx = (currentPlayerIdx + 1) % players.Length;

        currentPlayer.OnTurnEnd += _OnTurnComplete;

        UIManager.instance.OnTurnStarted(currentPlayer);

        currentPlayer.StartTurn();
    }

    void OnTurnComplete(Vector2Int move)
    {
        currentPlayer.OnTurnEnd -= _OnTurnComplete;
        gameBoard.SetTileState(move.x, move.y, currentPlayer.Type);

        TileType? winType;
        if (gameBoard.CheckGameOver(move, out winType))
        {
            OnGameOver?.Invoke(winType == null ? null : players[currentPlayerIdx]);
            OnGameOver = null;
            return;
        }

        StartNextTurn(move);
    }
}
