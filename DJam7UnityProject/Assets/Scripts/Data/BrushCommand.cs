using System.Collections.Generic;
using UnityEngine;

public class BrushCommand
{
    public Vector2Int position;
    public Brush.TileDestination[] toPlace;
    public Brush.TileDestination[] oldTiles;
    public int pointsGained;

    public GameObject instantiatedBrush;

    public void Do(GameContext context)
    {
        List<Vector2Int> positions = new List<Vector2Int>();
        List<Brush.TileDestination> olds = new List<Brush.TileDestination>();
        foreach (var td in toPlace)
        {
            var pos = position + td.destination;
            olds.Add(new Brush.TileDestination()
            {
                destination = td.destination,
                tile = context.state.GetTileAt(pos),
            });
            context.state.SetTileAt(pos,td.tile);
            positions.Add(pos);
        }
        instantiatedBrush = context.state.view.CreateBrushObject(this);
        pointsGained = context.ComputeScoreFor(positions);
        context.score += pointsGained;

        oldTiles = olds.ToArray();
    }

    public void Undo(GameContext context)
    {
        foreach (var td in oldTiles)
        {
            var pos = position + td.destination;
            context.state.SetTileAt(pos, td.tile);
        }
        GameObject.Destroy(instantiatedBrush);
        context.score -= pointsGained;
    }

    
}
