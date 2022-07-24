using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectSettings : MonoBehaviour
{
    public MapToLoad toLoad;
    public TMPro.TextMeshProUGUI buttonText;

    
    public int ID;
    public Map mapToLoad;
    
    private void OnValidate()
    {
        if(buttonText != null)
            buttonText.text = ID.ToString();
    }

    public void SelectMap()
    {
        var go = new GameObject("MapOverrider");
        go.AddComponent<MapOverrider>().overrideMap = mapToLoad;
        DontDestroyOnLoad(go);
    }
}
