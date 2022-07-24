using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DominoQueueView : MonoBehaviour
{
    public Image left;
    public Image right;

    public void Set(Brush brush)
    {
        left.sprite = brush.tiles[0].tile.image;
        right.sprite = brush.tiles[1].tile.image;
    }
}
