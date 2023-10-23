using UnityEngine;

[CreateAssetMenu(menuName = "Data/String", fileName = "String")]
public class DataString : DataBase<string>, IVariable<string>
{
    [field: SerializeField] public string SO_Value { get; set; }
    public override string DataValue { get { return SO_Value; } set { SO_Value = value; } }
}
