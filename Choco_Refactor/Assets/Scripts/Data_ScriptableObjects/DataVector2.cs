using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Vector2", fileName = "Vector2")]

public class DataVector2 : DataBase<Vector2>, IVariable<Vector2>
{
    [field: SerializeField] public Vector2 SO_Value { get; set; }
    public override Vector2 DataValue { get { return SO_Value; } set { SO_Value = value; } }
}
