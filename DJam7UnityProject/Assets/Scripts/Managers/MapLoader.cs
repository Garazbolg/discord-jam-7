using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-900)]
public class MapLoader : MonoBehaviour
{
    public MapToLoad toLoad;
    public MapView mapView;

    IEnumerator Start()
    {
        if (toLoad.map == null)
            toLoad.map = toLoad.defaultMap;
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
