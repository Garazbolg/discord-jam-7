using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EditorPlayerController : PlayerController
{
    protected override bool isEditor => true;

    protected override IEnumerator Start()
    {
        yield return null;
        StartCoroutine(base.Start());
        currentBrush = set.brushes[0];
    }
}
