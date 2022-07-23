using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateLoader : MonoBehaviour
{
    private static ApplicationState currentState = null;
    public ApplicationState stateToLoad;

    public void Load()
    {
        if(currentState != null)
            currentState.Unload();
        currentState = stateToLoad;
        stateToLoad.Load();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
