using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/New Tile")]
public class TileAsset : ScriptableObject
{
    public Sprite image;
    public Sprite[] happyImage;
    public GameObject prefab;
    public List<TileAsset> canOverrideTiles;
    public List<TileAsset> propagatesTo;
    public int starCount = 0;
    public int scoreValue = 1;
}
