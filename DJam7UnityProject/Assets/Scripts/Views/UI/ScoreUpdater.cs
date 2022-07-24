using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    private int targetScore;
    private int targetCurrentScore;
    private int current = 0;
    public float speed;

    private int oldScore = 0;

    private void Start()
    {
        current = targetCurrentScore = targetScore = 0;
        text.text = GetString();
    }

    void Update()
    {
        targetScore = GameManager.Instance.context.score;
        if (targetScore != targetCurrentScore)
        {
            StopAllCoroutines();
            oldScore = targetCurrentScore;
            targetCurrentScore = targetScore;
            StartCoroutine(Co_Anim());
        }
    }

    IEnumerator Co_Anim()
    {
        float duration = 1f / speed;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            current = Mathf.FloorToInt(Mathf.Lerp(oldScore, targetCurrentScore + 0.1f, time / duration));
            text.text = GetString();
            yield return null;
        }

        current = targetCurrentScore;
        text.text = GetString();
    }

    private string GetString()
    {
        return $"{current}/{GameManager.Instance.context.targetScore}";
    }
}
