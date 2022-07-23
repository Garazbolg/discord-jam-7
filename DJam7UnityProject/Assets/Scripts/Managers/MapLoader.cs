using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public MapToLoad toLoad;
    public MapView mapView;

    void Start()
    {
        GameManager.Instance.context.LoadMap(toLoad.map);
        GameManager.Instance.context.state.view = mapView;
        mapView.Init();
    }
}
