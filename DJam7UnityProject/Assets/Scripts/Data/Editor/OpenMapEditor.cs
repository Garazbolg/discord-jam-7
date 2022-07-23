using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;

public class OpenMapEditor : UnityEditor.EditorWindow
{
    // Note that we pass the same path, and also pass "true" to the second argument.
    [MenuItem("Assets/Map/Play", true)]
    public static bool ValidatePlayMap()
    {
        return Selection.activeObject is Map;
    }
    [MenuItem("Assets/Map/Play")]
    public static void PlayMap()
    {
        Open((Map)Selection.activeObject,false);
    }

    [MenuItem("Assets/Map/Edit", true)]
    public static bool ValidateEditMap()
    {
        return Selection.activeObject is Map;
    }
    [MenuItem("Assets/Map/Edit")]
    public static void EditMap()
    {
        Open((Map)Selection.activeObject,true);
    }

    public static void Open(Map state, bool isEditor)
    {
        var mapToLoad = AssetDatabase.LoadAssetAtPath<MapToLoad>("Assets/Data/Context/MapToLoad.asset");
        mapToLoad.map = state;
        
        Open(AssetDatabase.LoadAssetAtPath<ApplicationState>(
            isEditor ? "Assets/Data/ApplicationStates/EditTimeEditor.asset"
            : "Assets/Data/ApplicationStates/Game.asset"));
        
        EditorApplication.isPlaying = true;
    }
    
    public static void Open(ApplicationState state)
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        UnityEngine.SceneManagement.Scene blank = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);

        state.Load();

        EditorSceneManager.UnloadScene(blank);
    }
}
