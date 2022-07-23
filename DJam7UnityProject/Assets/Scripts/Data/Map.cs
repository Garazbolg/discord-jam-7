using UnityEngine;


[CreateAssetMenu(menuName = "Game/New Map")]
public class Map : ScriptableObject
{
    public Vector2Int size;
    public TileSet tileSet;
    public TileArray[] tiles;

    public MapView view;

    public TileAsset GetTileAt(Vector2Int position)
    {
        return GetTileAt(position.x, position.y);
    }

    public TileAsset GetTileAt(int x, int y)
    {
        if (x < 0 || x >= size.x || y < 0 || y >= size.y)
            return null;
        return tiles[x].tiles[y];
    }

    public void SetTileAt(Vector2Int position, TileAsset tile)
    {
        SetTileAt(position.x, position.y, tile);
    }
    public void SetTileAt(int x, int y, TileAsset tile)
    {
        if (x < 0 || x >= size.x || y < 0 || y >= size.y)
            return;
        tiles[x].tiles[y] = tile;
    }

    [System.Serializable]
    public struct TileArray
    {
        public TileAsset[] tiles;
    }
}
