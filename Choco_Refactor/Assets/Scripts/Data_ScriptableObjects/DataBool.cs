using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Bool", fileName = "Bool")]
public class DataBool : DataBase<bool>, IVariable<bool>
{
    [field: SerializeField] public bool SO_Value { get; set; }
    public override bool DataValue { get { return SO_Value; } set { SO_Value = value; } }
}
