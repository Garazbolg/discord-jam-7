using System.Collections.Generic;
using UnityEngine;

public class BrushCommand
{
    public Vector2Int position;
    public Brush.TileDestination[] toPlace;
    public Brush.TileDestination[] oldTiles;
    public int pointsGained;

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
        context.state.view.CreateBrushObject(this,false);
        var affected = context.GetAffected(positions);
        context.state.view.PropagateAnim(affected,position);
        pointsGained = context.ComputeScoreFor(affected);
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
        context.state.view.CreateBrushObject(this, true);
        context.score -= pointsGained;
    }
}
