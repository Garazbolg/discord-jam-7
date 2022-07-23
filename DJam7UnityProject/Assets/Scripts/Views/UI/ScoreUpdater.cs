using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    
    void Update()
    {
        text.text = GameManager.Instance.context.score.ToString();
    }
}
