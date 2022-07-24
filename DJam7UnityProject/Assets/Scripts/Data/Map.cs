using System;
using System.Buffers.Text;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/New Map")]
public class Map : ScriptableObject
{
    public Vector2Int size;
    public BrushSet overrideSet;
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

    public static string Export(Map map)
    {
        List<byte> bytes = new List<byte>();
        int index = 0;
        byte current = 0;
        var tileSet = GameManager.Instance.context.constants.tileset;
        foreach (var ta in map.tiles)
        {
            foreach (var tile in ta.tiles)
            {
                byte id = 0;
                if (tile == tileSet.ground) id = 0;
                else if (tile == tileSet.wall) id = 1;
                else if (tile == tileSet.red) id = 2;
                else if (tile == tileSet.blue) id = 3;
                else if (tile == tileSet.green) id = 4;
                else if (tile == tileSet.orange) id = 5;
                else if (tile == tileSet.white) id = 6;
                else if (tile == tileSet.black) id = 7;
                else
                {
                    throw new Exception("Unknown tile : " + tile.name);
                }

                if (index == 0)
                {
                    current |= (byte)(id << 4);
                    index++;
                }
                else
                {
                    current |= id;
                    index = 0;
                    bytes.Add(current);
                    current = 0;
                }
            }
        }
        if(index == 1)
            bytes.Add(current);

        return Convert.ToBase64String(bytes.ToArray());
    }

    public static Map Import(string s)
    {
        var map = CreateInstance<Map>();
        map.size = new Vector2Int(15,15);
        map.overrideSet = null;
        
        map.tiles = new Map.TileArray[map.size.x];
        for (int i = 0; i < map.size.x; i++)
        {
            map.tiles[i].tiles = new TileAsset[map.size.y];
        }
        
        var bytes = Convert.FromBase64String(s);
        var tileSet = GameManager.Instance.context.constants.tileset;
        for (var x = 0; x < 15; x++)
        {
            for (var y = 0; y < 15; y++)
            {
                var index = x * 15 + y;
                var b = (index % 2 == 0) ? bytes[index / 2] >> 4 : bytes[index/2] & 0x0f;
                map.tiles[x].tiles[y] = byteToTile(b,tileSet);
            }
        }

        return map;
    }

    private static TileAsset byteToTile(int b,TileSet tileSet)
    {
        TileAsset tile = null;
        switch (b)
        {
            case 0 : tile = tileSet.ground; break;
            case 1 : tile = tileSet.wall; break;
            case 2 : tile = tileSet.red; break;
            case 3 : tile = tileSet.blue; break;
            case 4 : tile = tileSet.green; break;
            case 5 : tile = tileSet.orange; break;
            case 6 : tile = tileSet.white; break;
            case 7 : tile = tileSet.black; break;
            default: throw new Exception("Unknown tile Id : " + b.ToString());
        }
        return tile;
    }
}
