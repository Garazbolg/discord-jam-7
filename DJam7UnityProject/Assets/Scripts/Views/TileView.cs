using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TileView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer backSpriteRenderer;

    [NonSerialized] public Sprite normal;
    [NonSerialized] public Sprite[] happy;

    public AnimationCurve heightAnim;
    [Min(.01f)]
    public float duration = 1f;

    public float frameTime => .04f;

    public void SetSprite(Sprite sprite,Sprite[] shappy, int order)
    {
        normal = sprite;
        happy = shappy;
        spriteRenderer.sprite = normal;
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
        var offset = Random.Range(0, happy.Length);
        spriteRenderer.sprite = happy[offset];
        
        while (time < duration)
        {
            time += Time.deltaTime;
            var index = (Mathf.FloorToInt(time / frameTime) + offset) % happy.Length;
            spriteRenderer.sprite = happy[index];
            spriteRenderer.transform.position = finalPos + Vector3.up * heightAnim.Evaluate(time / duration);
            yield return null;
        }

        spriteRenderer.sprite = normal;
        spriteRenderer.transform.position = finalPos;
        yield return null;
    }
}
