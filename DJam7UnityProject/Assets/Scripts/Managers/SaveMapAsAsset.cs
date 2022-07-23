using UnityEngine;

public class SaveMapAsAsset : MonoBehaviour
{
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            UnityEditor.AssetDatabase.CreateAsset(GameManager.Instance.context.state, UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/Data/Maps/NewMap.asset"));
        }
    }
#endif
}
