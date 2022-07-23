using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public MapToLoad toLoad;
    public MapView mapView;

    IEnumerator Start()
    {
        GameManager.Instance.context.LoadMap(toLoad.map);
        GameManager.Instance.context.state.view = mapView;
        mapView.Init();

        PlayerController pc = null;
        while ((pc = FindObjectOfType<PlayerController>()) == null)
        {
            yield return null;
        }
        if (toLoad.map.overrideSet != null)
        {
            pc.set = toLoad.map.overrideSet;
        }
    }
}
