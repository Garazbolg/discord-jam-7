using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer backSpriteRenderer;

    public void SetSprite(Sprite sprite, int order)
    {
        spriteRenderer.sprite = sprite;
        backSpriteRenderer.sortingOrder = order;
        spriteRenderer.sortingOrder = order + 1;
    }
}
