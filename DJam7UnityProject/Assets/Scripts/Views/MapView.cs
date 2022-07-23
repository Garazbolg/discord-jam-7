using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    public GameObject CreateBrushObject(BrushCommand command)
    {
        var go = new GameObject();
        go.transform.parent = transform;
        go.transform.position = new Vector3(command.position.x, command.position.y);
        foreach (var td in command.toPlace)
        {
            var tileView = Instantiate(GameManager.Instance.context.constants.TilePrefab,go.transform);
            tileView.transform.localPosition = new Vector3(td.destination.x, td.destination.y);
            tileView.SetSprite(td.tile.image,2);
        }

        return go;
    }

    public GameObject CreateBrushObject(Brush brush, Vector2Int position, bool isValid = true)
    {
        var prefab = isValid
            ? GameManager.Instance.context.constants.TilePrefab
            : GameManager.Instance.context.constants.InvalidTilePrefab;
        var go = new GameObject();
        go.transform.position = new Vector3(position.x, position.y);
        foreach (var td in brush.tiles)
        {
            var tileView = Instantiate(prefab,go.transform);
            tileView.transform.localPosition = new Vector3(td.destination.x, td.destination.y);
            tileView.SetSprite(td.tile.image,4);
        }

        return go;
    }

    public void Init()
    {
        if (GameManager.Instance == null) throw new System.Exception("No GameManager Instance");
        if (GameManager.Instance.context == null) throw new System.Exception("No Context");
        if (GameManager.Instance.context.state == null) throw new System.Exception("No Map Loaded");
        if (GameManager.Instance.context.state.tiles == null) throw new System.Exception("No Map Tiles");
        for (int i = 0; i < GameManager.Instance.context.state.size.x; i++)
        {
            for (int j = 0; j < GameManager.Instance.context.state.size.y; j++)
            {
                var tile = GameManager.Instance.context.state.GetTileAt(i, j);
                var go = new GameObject();
                go.transform.parent = transform;
                go.transform.position = new Vector3(i, j);
                var tileView = Instantiate(GameManager.Instance.context.constants.TilePrefab, go.transform);
                tileView.SetSprite(tile.image,0);
            }
        }
    }
}
