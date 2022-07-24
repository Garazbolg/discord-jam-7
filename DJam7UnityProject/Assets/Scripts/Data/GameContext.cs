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
    public List<Brush> brushQueue = new List<Brush>();
    public PlayerController player;
    public GameObject WinUI;
    public int targetScore;

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

    public int ComputeScoreFor(List<Vector2Int> affected)
    {
        int sum = 0;
        int sumStars = 0;
        foreach (var pos in affected)
        {
            sum += state.GetTileAt(pos).scoreValue;
            sumStars += GetStarCount(pos);
        }

        return sum * (1+sumStars);
    }

    public List<Vector2Int> GetAffected(List<Vector2Int> modifiedPositions)
    {
        visitedPoints.Clear();
        foreach (var pos in modifiedPositions)
        {
            GetComboTargets(pos, state.GetTileAt(pos));
        }
        var list = new List<Vector2Int>(visitedPoints);
        visitedPoints.Clear();

        return list;
    }

    private void GetComboTargets(Vector2Int position, TileAsset tileBase)
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

            if (tileBase.propagatesTo.Contains(t2))
                GetComboTargets(pos2, tileBase);
        }
    }

    private int GetStarCount(Vector2Int position)
    {
        return state.GetTileAt(position).starCount;
    }
}
