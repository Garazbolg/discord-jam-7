using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLoader : MonoBehaviour
{
    public ApplicationState stateToLoad;

    public void Load()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Blank");

        stateToLoad.Load();
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Blank");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
