using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterfaceChecker : ScriptableObject
{
    public abstract Component CheckInterface(GameObject passIn);
}