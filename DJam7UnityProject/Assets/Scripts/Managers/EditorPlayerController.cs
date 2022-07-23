using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EditorPlayerController : PlayerController
{
    public int index = 0;

    protected override void Start()
    {
        base.Start();
        currentBrush = set.brushes[index];
    }

    protected override void Update()
    {
        base.Update();
        index += Mathf.FloorToInt(Input.mouseScrollDelta.y) + set.brushes.Length;
        index = index % set.brushes.Length;
        currentBrush = set.brushes[index];
    }
}
