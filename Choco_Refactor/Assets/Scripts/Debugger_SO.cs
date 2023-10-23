using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Debugger_SO : ScriptableObject
{
    public void DebugLog(string message)
    {
        Debug.Log(message);
    }
}
