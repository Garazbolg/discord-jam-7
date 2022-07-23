using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/New Brush")]
public class Brush : ScriptableObject
{
    [System.Serializable]
    public struct TileDestination
    {
        public TileAsset tile;
        public Vector2Int destination;
    }

    public TileDestination[] tiles;

    public void RotateBrush()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].destination = RotateVector(tiles[i].destination);
        }
    }

    public static Vector2Int RotateVector(Vector2Int source)
    {
        // Clockwise rotation
        return new Vector2Int(source.y, -source.x);
    }

    public bool CanUse(GameContext context, Vector2Int position, bool isEditor = false)
    {
        var flag = isEditor;
        foreach (var td in tiles)
        {
            var t = td.tile;
            var target = context.state.GetTileAt(position + td.destination);
            if (target == null)
                return false;
            if (!isEditor && !t.canOverrideTiles.Contains(target))
                return false;

            if (flag) continue;
            foreach (var dir in context.constants.directions)
            {
                var adj = context.state.GetTileAt(position + td.destination + dir);
                if (adj.propagatesTo.Contains(t))
                {
                    flag = true;
                    break;
                }
            }
        }
        return flag;
    }

    public BrushCommand GetCommand(GameContext context, Vector2Int position)
    {
        var replaced = new List<TileDestination>();
        foreach (var td in tiles)
        {
            var t = td.tile;
            var target = context.state.GetTileAt(position + td.destination);
            replaced.Add(new TileDestination()
            {
                destination = td.destination,
                tile = target,
            });
        }

        return new BrushCommand()
        {
            position = position,
            toPlace = tiles.Clone() as TileDestination[],
            oldTiles = replaced.ToArray(),
        };
    }
}
