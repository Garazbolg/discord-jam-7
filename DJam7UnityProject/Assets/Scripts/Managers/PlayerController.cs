using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public BrushSet set;
    public bool randomFirst = false;
    
    public Brush currentBrush;
    private Camera cam;
    private readonly Stack<BrushCommand> done = new Stack<BrushCommand>();
    private readonly Stack<BrushCommand> unDone = new Stack<BrushCommand>();
    private GameObject preview;

    protected virtual bool isEditor => false;
    
    protected virtual void Start()
    {
        cam = Camera.main;
        currentBrush = randomFirst ? set.brushes[Random.Range(0,set.brushes.Length)]: set.brushes[0];
    }

    protected virtual void Update()
    {
        var point = cam.ScreenToWorldPoint(Input.mousePosition);
        point += Vector3.one/2; //Offset because tile pivot is center
        var position = new Vector2Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y));
        bool usable = currentBrush.CanUse(GameManager.Instance.context, position,isEditor);

        if(preview != null)
            Destroy(preview);
        if(GameManager.Instance.context.state.GetTileAt(position) != null)
            preview = GameManager.Instance.context.state.view.CreateBrushObject(currentBrush, position,usable);

        if(Input.GetMouseButtonDown(1))
            Rotate();

        if (usable && Input.GetMouseButtonDown(0))
        {
            var comand = currentBrush.GetCommand(GameManager.Instance.context, position);
            comand.Do(GameManager.Instance.context);
            done.Push(comand);
            unDone.Clear();
            if(!isEditor)
                currentBrush = set.brushes[Random.Range(0, set.brushes.Length)];
        }

        if (done.Count > 0 && Input.GetMouseButtonDown(2))
        {
            Undo();
        }
    }
    public bool CanUndo()
    {
        return done.Count > 0;
    }

    public void Undo()
    {
        
        
        var command = done.Pop();
        command.Undo(GameManager.Instance.context);
        unDone.Push(command);
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
    }

    public void Rotate()
    {
        currentBrush.RotateBrush();
    }

    
}
