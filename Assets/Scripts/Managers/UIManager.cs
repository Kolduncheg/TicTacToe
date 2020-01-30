using UnityEngine;
using UnityEngine.UI;

class UIManager : MonoBehaviour
{
    public Text GUIText;
    public Button Restart;

    string currentTurn = "{0} - turn";
    string winText = "{0} - WINNER!";
    string drawText = "DRAW";

    public static UIManager instance;

    public void Awake()
    {
        instance = this;
    }

    public void OnTurnStarted(IPlayer player)
    {
        char playerTurn = player.Type == TileType.Circle ? 'O' : 'X';
        GUIText.text = string.Format(currentTurn, playerTurn);
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame();
    }

    public void OnGameEnd(IPlayer player)
    {
        if (player == null)
        {
            GUIText.text = drawText;
        }
        else
        {
            char playerTurn = player.Type == TileType.Circle ? 'O' : 'X';
            GUIText.text = string.Format(winText, playerTurn);
        }
    }

}
