using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EditorPlayerController : PlayerController
{
    protected override bool isEditor => true;

    protected override void Start()
    {
        base.Start();
        currentBrush = set.brushes[0];
    }
}
