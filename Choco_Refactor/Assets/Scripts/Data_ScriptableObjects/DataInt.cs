using UnityEngine;

[CreateAssetMenu(menuName = "Data/Int", fileName = "Int")]
public class DataInt : DataBase<int>, IVariable<int>
{
    [field: SerializeField] public int SO_Value { get; set; }
    public override int DataValue { get { return SO_Value; } set { SO_Value = value; } }
}
