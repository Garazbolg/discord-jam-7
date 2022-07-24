using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHover : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent OnHoverEvent = new UnityEvent();


    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEvent?.Invoke();
    }
}
