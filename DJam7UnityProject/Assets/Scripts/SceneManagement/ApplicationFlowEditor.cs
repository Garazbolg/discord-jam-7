using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
public class ApplicationFlowEditor : UnityEditor.EditorWindow
{
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if(Selection.activeObject is ApplicationState)
        {
            Open((ApplicationState)Selection.activeObject);
            return true;
        }
        return false;
    }

    public static void Open(ApplicationState state)
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        UnityEngine.SceneManagement.Scene blank = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);

        state.Load();

        EditorSceneManager.UnloadSceneAsync(blank);
    }
}
