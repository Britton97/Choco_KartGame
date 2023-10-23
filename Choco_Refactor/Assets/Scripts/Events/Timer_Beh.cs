using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Timer_Beh : MonoBehaviour
{
    [SerializeField] public UnityEvent onTimerEnd;

    public void StartTimer(float time)
    {
        StartCoroutine(Timer(time));
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        onTimerEnd.Invoke();
    }
}
