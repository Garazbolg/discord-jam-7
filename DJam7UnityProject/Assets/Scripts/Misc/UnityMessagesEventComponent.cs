using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityMessagesEventComponent : MonoBehaviour
{
    public UnityEvent OnAwake;
    public UnityEvent OnStart;
    public UnityEvent OnPostStart;
    public UnityEvent OnEnabled;
    public UnityEvent OnDisabled;
    public UnityEvent OnUpdate;
    public UnityEvent OnDestroyed;

    private bool _postStartDone = false;

    private void Awake()
    {
        OnAwake?.Invoke();
    }

    private void Start()
    {
        OnStart?.Invoke();
    }
	
    private void Update()
    {
        if (!_postStartDone) {
            OnPostStart?.Invoke();
            _postStartDone = true;
        }

        OnUpdate?.Invoke();
    }

    private void OnEnable()
    {
        OnEnabled?.Invoke();
    }

    private void OnDisable()
    {
        OnDisabled?.Invoke();
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}