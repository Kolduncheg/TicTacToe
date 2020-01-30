using UnityEngine;

class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public BoardView boardView;

    public bool EditorDisabled = false;

    GameBoardController gameBoard;
    TurnManager turnManager;
    Player player1;
    Player player2;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (EditorDisabled == false)
        {
            if (Input.GetKeyDown(KeyCode.F12))
            {
                EditorManager.instance.Active = !EditorManager.instance.Active;
            }
        }
    }

    public void StartGame(int Rows = 3, int Cols = 3)
    {
        gameBoard = new GameBoardController(boardView, new BoardModel(Rows, Cols));

        player1 = new Player(gameBoard, TileType.Cross);
        player2 = new Player(gameBoard, TileType.Circle);

        turnManager = new TurnManager(gameBoard, player1, player2);

        turnManager.Start();

        turnManager.OnGameOver += UIManager.instance.OnGameEnd;
    }

    public void RestartGame()
    {
        if (gameBoard != null)
            StartGame(gameBoard.Rows, gameBoard.Cols);
    }
}