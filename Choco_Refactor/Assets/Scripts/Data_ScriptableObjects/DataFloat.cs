using System;
using UnityEngine;
using UnityEngine.Scripting;

[CreateAssetMenu(menuName = "Data/Float", fileName = "Float")]
public class DataFloat : DataBase<float>, IVariable<float>
{
    [field: SerializeField] public float SO_Value { get; set; }
    public override float DataValue { get { return SO_Value; } set { SO_Value = value; } }

}