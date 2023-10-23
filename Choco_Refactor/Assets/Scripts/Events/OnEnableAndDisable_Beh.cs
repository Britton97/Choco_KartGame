using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnableAndDisable_Beh : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnableEvent;
    [SerializeField] private UnityEvent onDisableEvent;
    private void OnEnable()
    {
        onEnableEvent.Invoke();
    }

    private void OnDisable()
    {
        try { onDisableEvent.Invoke(); } catch { }
    }
}
