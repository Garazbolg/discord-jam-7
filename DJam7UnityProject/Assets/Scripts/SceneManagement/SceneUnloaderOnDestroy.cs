using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUnloaderOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        var scenes = Resources.FindObjectsOfTypeAll<SceneReferenceScriptableObject>();
        foreach(var scene in scenes)
        {
            scene.Unload();
        }
    }
}
