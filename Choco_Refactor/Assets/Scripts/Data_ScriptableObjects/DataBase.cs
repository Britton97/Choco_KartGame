using UnityEngine;
public abstract class DataBase<T> : ScriptableObject
{
    public abstract T DataValue { get; set; }
}