using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ImportString : MonoBehaviour
{
    public TMP_InputField inputField;
    public MapToLoad toLoad;
    public Button PlayCustom;

    public void LoadCustom()
    {
        var go = new GameObject("MapOverrider");
        go.AddComponent<MapOverrider>().overrideMap = Map.Import(inputField.text);
        DontDestroyOnLoad(go);
    }

    public void UpdateInteractable()
    {
        try
        {
            Map.Import(inputField.text);
            PlayCustom.interactable = true;
        }
        catch (Exception e)
        {
            PlayCustom.interactable = false;
        }
    }
}
