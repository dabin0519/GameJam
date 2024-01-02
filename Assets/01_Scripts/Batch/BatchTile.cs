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
        Vector2 pos = Vector2Int.CeilToInt(position);
        pos += new Vector2(-0.5f, -0.5f);
        return pos;
    }
}
