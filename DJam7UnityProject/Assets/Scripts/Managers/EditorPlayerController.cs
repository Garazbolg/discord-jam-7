using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EditorPlayerController : PlayerController
{
    protected override bool isEditor => true;
    protected override bool IsInputPlace => Input.GetMouseButton(0);

    protected override IEnumerator Start()
    {
        yield return null;
        StartCoroutine(base.Start());
        yield return null;
        yield return null;
        currentBrush = set.brushes[0];
    }
}
