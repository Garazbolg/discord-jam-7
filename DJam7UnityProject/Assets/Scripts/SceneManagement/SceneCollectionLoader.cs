using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCollectionLoader : MonoBehaviour
{
    public SceneReferenceScriptableObject[] ScenesToLoad;
    public bool LoadOnStart = false;

    private void Start()
    {
        if (LoadOnStart)
            Load();
    }

    public void Load()
    {
        for (int i = 0; i < ScenesToLoad.Length; i++)
        {
            ScenesToLoad[i].LoadAdditive();
        }
    }

    public void Unload()
    {
        for (int i = 0; i < ScenesToLoad.Length; i++)
        {
            ScenesToLoad[i].Unload();
        }
    }
}
