using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/New Brush set")]
public class BrushSet : ScriptableObject
{
    public enum NextMethod
    {
        Sequence,
        Random,
    }

    public NextMethod nextMethod;
    public Brush[] brushes;

    private int index = -1;

    public void Init()
    {
        index = -1;
    }

    public Brush GetNext()
    {
        switch (nextMethod)
        {
            case NextMethod.Sequence:
                index = (index + 1 + brushes.Length) % brushes.Length;
                Debug.Log($"Loading from set {name} at {index} => {brushes[index].name}");
                return brushes[index];
            case NextMethod.Random:
                return brushes[Random.Range(0, brushes.Length)];
            default:
                return null;
        }
    }
}
