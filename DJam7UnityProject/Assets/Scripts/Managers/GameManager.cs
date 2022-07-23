using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameContext context;

    private void Awake()
    {
        if(Instance != null)
        {
            throw new System.Exception("Already a game Manager instance running");
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
