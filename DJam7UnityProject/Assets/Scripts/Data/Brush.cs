using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/New Brush")]
public class Brush : ScriptableObject
{
    public Vector2Int size;
    
    [System.Serializable]
    public struct TileDestination
    {
        public TileAsset tile;
        public Vector2Int destination;
    }

    public TileDestination[] tiles;

    public void RotateBrush(bool clockwise)
    {
        throw new System.NotImplementedException();
    }

    public bool CanUse(GameContext context, Vector2Int position)
    {
        foreach (var td in tiles)
        {
            var t = td.tile;
            var target = context.state.GetTileAt(position + td.destination);
            if (target == null)
                return false;
            if (!t.canOverrideTiles.Contains(target))
                return false;
        }
        return true;
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
