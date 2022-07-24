using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class ExportString : MonoBehaviour
{
    public TMP_InputField text;

    private IEnumerator Start()
    {
        yield return null;
        yield return null;
        yield return null;
        var epc = GameManager.Instance.context.player;
        epc.onChangeEvent = UpdateString;
    }

    public void UpdateString()
    {
        text.text = Map.Export(GameManager.Instance.context.state);
    }
}
