using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[System.Serializable]
public class GameContext
{
    public Constants constants;
    public Map state;
    public int score;

    private HashSet<Vector2Int> visitedPoints;

    public void Init()
    {
        score = 0;
        visitedPoints = new HashSet<Vector2Int>();
    }

    public void LoadMap(Map map)
    {
        Init();
        state = ScriptableObject.Instantiate<Map>(map);
    }

    public int ComputeScoreFor(List<Vector2Int> modifiedPositions)
    {
        visitedPoints.Clear();
        foreach (var pos in modifiedPositions)
        {
            GetComboTargets(pos);
        }

        UnityEngine.Debug.Log($"Visited count: {visitedPoints.Count}");

        int sum = 0;
        int sumStars = 0;
        foreach (var pos in visitedPoints)
        {
            sum += state.GetTileAt(pos).scoreValue;
            sumStars += GetStarCount(pos);
        }

        return sum * (1+sumStars);
    }

    private void GetComboTargets(Vector2Int position)
    {
        if (visitedPoints.Contains(position)) return;
        var t1 = state.GetTileAt(position);
        if (t1 == null) return;
        visitedPoints.Add(position);

        foreach (var dir in constants.directions)
        {
            var pos2 = position + dir;
            var t2 = state.GetTileAt(pos2);
            if (t2 == null) continue;

            if (t1.propagatesTo.Contains(t2))
                GetComboTargets(pos2);
        }
    }

    private int GetStarCount(Vector2Int position)
    {
        return state.GetTileAt(position).starCount;
    }
}
