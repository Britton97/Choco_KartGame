using UnityEngine;
using UnityEngine.Events;

public abstract class DataReference<T>
{
    [SerializeField] private bool useConstant = true;
    public T constantValue;
    [SerializeField] private DataBase<T> data;
    public UnityEvent<T> dataEvent;

    public T Value
    {
        get
        {
            return useConstant ? constantValue : data.DataValue;
        }
    }
}

[System.Serializable]
public class FloatReference : DataReference<float> { }

[System.Serializable]
public class Vector2Reference : DataReference<Vector2> { }

[System.Serializable]
public class GameObjectReference : DataReference<GameObject> { }
[System.Serializable]
public class BoolReference : DataReference<bool> { }