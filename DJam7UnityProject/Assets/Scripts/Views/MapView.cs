using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class MapView : MonoBehaviour
{
    #region Editing

    public TileViewArray[] views;
    
    [System.Serializable]
    public struct TileViewArray
    {
        public TileView[] Views;
    }
    
    public void CreateBrushObject(BrushCommand command, bool isUndo)
    {
        var arr = isUndo ? command.oldTiles : command.toPlace;
        for (var index = 0; index < arr.Length; index++)
        {
            var td = arr[index];
            int x = command.position.x + td.destination.x;
            int y = command.position.y + td.destination.y;
            SetTile(x,y,td.tile,2);
        }
    }

    private void SetTile(int x, int y, TileAsset tile, int spriteOrder)
    {
        var tileView = Instantiate(GameManager.Instance.context.constants.TilePrefab, transform);
        tileView.transform.position = new Vector3(x,y);
        tileView.SetSprite(tile.image, spriteOrder);
        if(views[x].Views[y] != null)
            Destroy(views[x].Views[y].gameObject);
        views[x].Views[y] = tileView;
    }

    private TileView GetTile(int x, int y)
    {
        return views[x].Views[y];
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

    #endregion
    
    public void Init()
    {
        if (GameManager.Instance == null) throw new System.Exception("No GameManager Instance");
        if (GameManager.Instance.context == null) throw new System.Exception("No Context");
        if (GameManager.Instance.context.state == null) throw new System.Exception("No Map Loaded");
        if (GameManager.Instance.context.state.tiles == null) throw new System.Exception("No Map Tiles");
        
        Map map = GameManager.Instance.context.state;
        views = new TileViewArray[map.size.x];
        for (var index = 0; index < views.Length; index++)
        {
            views[index].Views = new TileView[map.size.y];
        }

        for (int i = 0; i < map.size.x; i++)
        {
            for (int j = 0; j < map.size.y; j++)
            {
                var tile = map.GetTileAt(i, j);
                if(tile != null)
                    SetTile(i,j,tile,0);
            }
        }
    }

    #region Animation

    public float propagationDelay = .75f;
    
    public void PropagateAnim(IEnumerable<Vector2Int> targets, Vector2Int origin)
    {
        StartCoroutine(CO_PropagationAnimation(new List<Vector2Int>(targets),origin));
    }

    IEnumerator CO_PropagationAnimation(List<Vector2Int> targets, Vector2Int origin)
    {
        var dist = FindFurthestDistance(targets,origin);
        List<Vector2Int>[] positions = new List<Vector2Int>[dist+1];
        for (int i = 0; i < dist+1; i++)
        {
            positions[i] = new List<Vector2Int>();
        }

        foreach (var target in targets)
        {
            positions[OrthoDistance(target,origin)].Add(target);
        }

        foreach (var list in positions)
        {
            foreach (var pos in list)
            {
                var tv = GetTile(pos.x, pos.y);
                tv.StartAnim();
            }

            yield return new WaitForSeconds(propagationDelay);
        }

        yield return null;
    }

    int OrthoDistance(Vector2Int a, Vector2Int b)
    {
        int x = Mathf.Abs(a.x - b.x);
        int y = Mathf.Abs(a.y - b.y);
        return x + y;
    }

    private int FindFurthestDistance(List<Vector2Int> targets, Vector2Int origin)
    {
        int distMax = 0;
        foreach (var vector2Int in targets)
        {
            distMax = Mathf.Max(distMax, OrthoDistance(origin, vector2Int));
        }

        return distMax;
    }

    #endregion
}
