public interface IPlayer
{
    TileType Type { get; }
    void PlayerType(TileType playerType);
    void StartTurn();
    event System.Action<UnityEngine.Vector2Int> OnTurnEnd;
}
