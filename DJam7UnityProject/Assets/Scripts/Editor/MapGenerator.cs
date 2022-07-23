using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName ="Unique/MapGenerator")]
public class MapGenerator: ScriptableObject
{
    public Vector2Int size;
    public TileSet tileSet;
    public string mapName;

    [ContextMenu("Create Map")]
    public void CreateMap()
    {
        Map map = CreateInstance<Map>();
        map.name = mapName;
        map.size = size;
        map.tiles = new Map.TileArray[size.x];
        for (int i = 0; i < size.x; i++)
        {
            map.tiles[i].tiles = new TileAsset[size.y];
        }

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                bool isEdge = (i == 0 || j == 0 || i == size.x - 1 || j == size.y - 1);
                var tile = isEdge ? tileSet.wall : tileSet.ground;
                map.SetTileAt(i,j,tile);
            }
        }
        AssetDatabase.CreateAsset(map, AssetDatabase.GenerateUniqueAssetPath($"Assets/Data/Maps/{mapName}.asset"));
        
    }
}