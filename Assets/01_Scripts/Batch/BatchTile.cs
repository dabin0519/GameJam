using UnityEngine;
using UnityEngine.Tilemaps;

public class BatchTile : MonoBehaviour
{
    public static BatchTile Instance;

    private Tilemap _tilemap;

    private void Awake()
    {
        Instance = this;

        _tilemap = GetComponent<Tilemap>();
    }

    public bool IsBatchObj(Vector2 position)
    {
        TileBase tileBase = null;
        tileBase = _tilemap.GetTile(_tilemap.WorldToCell(position));
        return tileBase;
    }

    public Vector2 Vector2IntPos(Vector2 position)
    {
        float x = Mathf.Round(position.x * 2) * 0.5f;
        float y = Mathf.Round(position.y * 2) * 0.5f;
        Vector2 pos = new Vector2(x, y);
        return pos;
    }
}
