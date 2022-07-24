using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-800)]
public class PlayerController : MonoBehaviour
{
    public BrushSet set;
    public float rotationDuration;
    public float placementDuration;
    
    public Brush currentBrush;
    private Camera cam = null;
    private readonly Stack<BrushCommand> done = new Stack<BrushCommand>();
    private readonly Stack<BrushCommand> unDone = new Stack<BrushCommand>();
    private GameObject preview;
    private bool enablePlayer = true;
    protected Vector2Int lastPoint = Vector2Int.zero;
    protected Vector2Int currentPoint;

    public virtual bool isEditor => false;

    protected virtual bool IsInputPlace => Input.GetMouseButtonDown(0);
    
    protected virtual IEnumerator Start()
    {
        yield return null;
        set.Init();
        cam = Camera.main;
        GameManager.Instance.context.player = this;
        GameManager.Instance.context.brushQueue.Clear();
        GameManager.Instance.context.brushQueue.Add(set.GetNext());
        GameManager.Instance.context.brushQueue.Add(set.GetNext());
        GameManager.Instance.context.brushQueue.Add(set.GetNext());
        SetNextBrush();
    }

    protected virtual void Update()
    {
        if(cam == null || !enablePlayer || currentBrush == null || GameManager.Instance == null) return;
        
        var point = cam.ScreenToWorldPoint(Input.mousePosition);
        point += Vector3.one/2; //Offset because tile pivot is center
        currentPoint = new Vector2Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y));
        bool usable = currentBrush.CanUse(GameManager.Instance.context, currentPoint, isEditor);

        if(preview != null)
            Destroy(preview);
        if(GameManager.Instance.context.state.GetTileAt(currentPoint) != null)
            preview = GameManager.Instance.context.state.view.CreateBrushObject(currentBrush, currentPoint, usable);

        if(Input.GetMouseButtonDown(1))
            Rotate();

        if (usable && IsInputPlace)
        {
            lastPoint = currentPoint;
            var comand = currentBrush.GetCommand(GameManager.Instance.context, currentPoint);
            comand.Do(GameManager.Instance.context);
            done.Push(comand);
            unDone.Clear();
            if (!isEditor)
                SetNextBrush();
            StartCoroutine(PlacementAnim());
            OnChanged();
        }

        if (done.Count > 0 && Input.GetMouseButtonDown(2))
        {
            Undo();
        }
    }

    public void SetNextBrush()
    {
        var queue = GameManager.Instance.context.brushQueue;
        currentBrush = queue[0];
        queue.RemoveAt(0);
        if(queue.Count < 3)
            queue.Add(set.GetNext());
    }

    public void ReverseBrush(Brush oldBrush)
    {
        GameManager.Instance.context.brushQueue.Insert(0,currentBrush);
        currentBrush = oldBrush;
    }

    #region Commands
    public bool CanUndo()
    {
        return done.Count > 0;
    }

    public void Undo()
    {
        var command = done.Pop();
        command.Undo(GameManager.Instance.context);
        unDone.Push(command);
        OnChanged();
    }
    
    public bool CanRedo()
    {
        return unDone.Count > 0;
    }
    
    public void Redo()
    {
        if(unDone.Count <= 0) return;
        
        var command = unDone.Pop();
        command.Do(GameManager.Instance.context);
        done.Push(command);
        OnChanged();
    }

    public void Rotate()
    {
        StartCoroutine(RotateAnim());
        currentBrush.RotateBrush();
    }
    #endregion

    #region Coroutines

    IEnumerator RotateAnim()
    {
        enablePlayer = false;
        float time = 0;
        while(time < rotationDuration)
        {
            time += Time.deltaTime;
            if(preview != null)
                preview.transform.rotation = Quaternion.Euler(0, 0, -90 * (time / rotationDuration));
            yield return null;
        }

        enablePlayer = true;
    }

    IEnumerator PlacementAnim()
    {
        enablePlayer = false;
        if (preview != null)
            Destroy(preview);

        yield return new WaitForSeconds(placementDuration);

        enablePlayer = true;
        yield break;
    }


    public Action onChangeEvent = null;
    public void OnChanged()
    {
        onChangeEvent?.Invoke();
    }

    #endregion
}
