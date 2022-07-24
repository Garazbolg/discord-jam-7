using UnityEngine;

[CreateAssetMenu(menuName ="Unique/Constants")]
public class Constants : ScriptableObject
{
    public Vector2Int[] directions;
    public TileView TilePrefab;
    public TileView InvalidTilePrefab;
    public TileSet tileset;
}
