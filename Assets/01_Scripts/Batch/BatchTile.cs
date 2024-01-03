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

    public void PlaseNewTileObj(Vector3 position, TileBase tile)
    {
        Vector3Int cellPosition = _tilemap.WorldToCell(position);
        _tilemap.SetTile(cellPosition, tile);
    }

    public bool IsBatchObj(Vector2 position)
    {
        TileBase tileBase = null;
        TileBase underBase = null;
        tileBase = _tilemap.GetTile(_tilemap.WorldToCell(position));
        underBase = _tilemap.GetTile(_tilemap.WorldToCell(position - new Vector2(0, 1f)));
        return tileBase || !underBase;
    }

    public Vector2 Vector2IntPos(Vector2 position)
    {
        Vector2 pos = Vector2Int.CeilToInt(position);
        pos += new Vector2(-0.5f, -0.5f);
        return pos;
    }
}
