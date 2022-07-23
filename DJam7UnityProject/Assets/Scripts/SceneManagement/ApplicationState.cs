using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ApplicationState : ScriptableObject
{
    public SceneReferenceScriptableObject[] scenes;

    public void Load()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i].LoadAdditive();
        }
    }

    public void Unload()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i].Unload();
        }
    }
}
