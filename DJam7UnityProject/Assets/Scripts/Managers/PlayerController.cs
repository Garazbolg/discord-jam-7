using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Brush currentBrush;
    private Camera cam;

    public Stack<BrushCommand> done = new Stack<BrushCommand>();

    private GameObject preview;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        var point = cam.ScreenToWorldPoint(Input.mousePosition);
        point += Vector3.one/2; //Offset because tile pivot is center
        var position = new Vector2Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y));
        bool usable = currentBrush.CanUse(GameManager.Instance.context, position);

        if(preview != null)
            Destroy(preview);
        if(GameManager.Instance.context.state.GetTileAt(position) != null)
            preview = GameManager.Instance.context.state.view.CreateBrushObject(currentBrush, position,usable);

        if(Input.GetMouseButtonDown(1))
            currentBrush.RotateBrush();

        if (usable && Input.GetMouseButtonDown(0))
        {
            var comand = currentBrush.GetCommand(GameManager.Instance.context, position);
            comand.Do(GameManager.Instance.context);
            done.Push(comand);
        }

        if (done.Count > 0 && Input.GetMouseButtonDown(2))
        {
            var comand = done.Pop();
            comand.Undo(GameManager.Instance.context);
        }
    }
}