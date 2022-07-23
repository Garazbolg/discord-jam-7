using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="New Scene Reference", menuName = "ScriptableObjects/Game/Scene Reference")]
public class SceneReferenceScriptableObject : ScriptableObject
{
#if UNITY_EDITOR
    public UnityEditor.SceneAsset SceneAsset;
#endif

    public string scenePath;
    public string SceneName;

    public void LoadAdditive()
    {
#if UNITY_EDITOR
        if(EditorApplication.isPlaying)
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        else
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scenePath, UnityEditor.SceneManagement.OpenSceneMode.Additive);
#else
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
#endif
    }

    public void Unload()
    {
#if UNITY_EDITOR
        if(EditorApplication.isPlaying)
            SceneManager.UnloadSceneAsync(SceneName,UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        else
            UnityEditor.SceneManagement.EditorSceneManager.CloseScene(UnityEditor.SceneManagement.EditorSceneManager.GetSceneByPath(scenePath),true);
#else
        SceneManager.UnloadSceneAsync(SceneName,UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
#endif
    }

    private void OnValidate()
    {
        scenePath = SceneAsset == null ? "" : UnityEditor.AssetDatabase.GetAssetPath(SceneAsset);
        SceneName = SceneAsset == null ? "" : SceneAsset.name;
    }
}
