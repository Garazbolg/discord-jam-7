using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EditorPlayerController : PlayerController
{
    public override bool isEditor => true;
    protected override bool IsInputPlace => Input.GetMouseButton(0) && lastPoint != currentPoint;


    protected override IEnumerator Start()
    {
        yield return null;
        StartCoroutine(base.Start());
        yield return null;
        yield return null;
        GameManager.Instance.context.player = this;
        currentBrush = set.brushes[0];
    }
}
