using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitSecondsBeforeAction : MonoBehaviour
{
    public UnityEvent afterAction;
    public float waitSeconds;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitSeconds);
        afterAction?.Invoke();
    }
}
