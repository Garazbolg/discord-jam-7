using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TileView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer backSpriteRenderer;

    [NonSerialized] public TileAsset oldTile;
    [NonSerialized] public Transform oldTileParent;

    public AnimationCurve heightAnim;
    [Min(.01f)]
    public float duration = 1f;

    public void SetSprite(Sprite sprite, int order)
    {
        spriteRenderer.sprite = sprite;
        backSpriteRenderer.sortingOrder = order;
        spriteRenderer.sortingOrder = order + 1;
    }

    public void StartAnim()
    {
        StartCoroutine(CO_ActivateAnimation());
    }

    IEnumerator CO_ActivateAnimation()
    {
        float time = 0;

        var finalPos = spriteRenderer.transform.position;

        while (time < duration)
        {
            time += Time.deltaTime;

            spriteRenderer.transform.position = finalPos + Vector3.up * heightAnim.Evaluate(time / duration);
            yield return null;
        }
        spriteRenderer.transform.position = finalPos;
        yield return null;
    }
}
