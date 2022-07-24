using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReference : MonoBehaviour
{
    private PlayerController pc;
    public Brush brushToSet;
    public Button buttonRef;
    public bool isUndo;
    
    private IEnumerator Start()
    {
        while ((pc = FindObjectOfType<PlayerController>()) == null)
        {
            yield return null;
        }
    }

    public void Undo()
    {
        pc.Undo();
    }
    
    public void Redo()
    {
        pc.Redo();
    }

    public void Rotate()
    {
        pc.Rotate();
    }

    public void SetBrush()
    {
        pc.currentBrush = brushToSet;
    }

    private void Update()
    {
        if (buttonRef)
        {
            buttonRef.interactable = isUndo ? pc.CanUndo() : pc.CanRedo();
        }
    }
}
