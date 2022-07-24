using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMovesView : MonoBehaviour
{
    public DominoQueueView[] queueViews;

    void Update()
    {
        for (int i = 0; i < queueViews.Length; i++)
        {
            if (GameManager.Instance.context.brushQueue.Count <= i)
                queueViews[i].gameObject.SetActive(false);
            else
            {
                queueViews[i].gameObject.SetActive(true);
                queueViews[i].Set(GameManager.Instance.context.brushQueue[i]);
            }
        }    
    }
}
