using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;

public class OpenMapEditor : UnityEditor.EditorWindow
{
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if(Selection.activeObject is Map)
        {
            Open((Map)Selection.activeObject);
            return true;
        }
        return false;
    }

    public static void Open(Map state)
    {
        var mapToLoad = AssetDatabase.LoadAssetAtPath<MapToLoad>("Assets/Data/Context/MapToLoad.asset");
        mapToLoad.map = state;
        Open(AssetDatabase.LoadAssetAtPath<ApplicationState>("Assets/Data/ApplicationStates/Game.asset"));
        
        EditorApplication.isPlaying = true;
    }
    
    public static void Open(ApplicationState state)
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        UnityEngine.SceneManagement.Scene blank = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);

        state.Load();

        EditorSceneManager.UnloadSceneAsync(blank);
    }
}
