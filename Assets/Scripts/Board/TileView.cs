using System;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button)), DisallowMultipleComponent]
public class TileView : MonoBehaviour, ITileView
{
    public Image img;
    Button button;

    public int Row { get; protected set; }
    public int Col { get; protected set; }
    
    public event Action<ITileView> OnClick;

    public void Init(int row, int col)
    {
        Row = row;
        Col = col;

        name = $"Tile_{Row}x{Col}";

        button = GetComponent<Button>();
        button?.onClick.AddListener( () => { OnClick?.Invoke(this); });
    }

    public void UpdateTile(Sprite Icon, Color color, bool interactable)
    {
        img.sprite = Icon;
        img.color = color;
        button.interactable = interactable;
    }
}

