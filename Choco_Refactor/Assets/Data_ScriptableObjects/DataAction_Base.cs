using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAction_Base<T> : GameAction
{
    [SerializeField] public DataBase<T> data;

    public void SetData(T pass)
    {
        data.DataValue = pass;
    }

    public T ReturnData()
    {
        return data.DataValue;
    }
}