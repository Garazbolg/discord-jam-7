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
        MapOverrider mapOverrider = null;
        Map loadTarget = null;
        if ((mapOverrider = FindObjectOfType<MapOverrider>()) != null)
        {
            loadTarget = mapOverrider.overrideMap;
            Destroy(mapOverrider.gameObject);
        }
        else
        {
            if (toLoad.map == null)
                toLoad.map = toLoad.defaultMap;
            loadTarget = toLoad.map;
        }
        
        GameManager.Instance.context.LoadMap(loadTarget);
        GameManager.Instance.context.state.view = mapView;
        mapView.Init();

        PlayerController pc = null;
        while ((pc = FindObjectOfType<PlayerController>()) == null)
        {
            yield return null;
        }
        if (loadTarget.overrideSet != null)
        {
            pc.set = loadTarget.overrideSet;
        }
    }
}
