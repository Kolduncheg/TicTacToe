using UnityEngine;

class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    private Rect windowRect = new Rect(10,10, 200, 120);
    private int Rows = 3;
    private int Cols = 3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public bool Active { get {return enabled;} set {enabled = value;} }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void OnGUI() => windowRect = GUI.Window(0, windowRect, WindowMain, "Editor");

    private void WindowMain(int id)
    {
        GUILayout.BeginHorizontal();
            GUILayout.Label("Rows = ", GUILayout.Width(50));
            Rows = int.Parse(GUILayout.TextField(Rows.ToString(), GUILayout.Width(30)));
            GUILayout.Label("Cols = ", GUILayout.Width(50));
            Cols = int.Parse(GUILayout.TextField(Cols.ToString(), GUILayout.Width(30)));
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Crate field"))
        {
            CreateField(Rows, Cols);
        }

       GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save config"))
        {
            if (Rows > 0 && Cols > 0)
            {
                XmlManager.instance.SaveLevelConfig(Rows, Cols);
            }
        }

        if (GUILayout.Button("Load config"))
        {
            Vector2Int fieldSize = XmlManager.instance.LoadLevelConfig();
            Rows = fieldSize.x;
            Cols = fieldSize.y;
            CreateField(Rows, Cols);
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("F12 - on/off editor");
    }

    private void CreateField(int rows, int cols)
    {
        GameManager.instance.StartGame(rows, cols);
    }
}