using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/GameObject", fileName = "Data GameObject")]
public class DataGameObject : DataBase<GameObject>, IVariable<GameObject>
{
    [field: SerializeField] public GameObject SO_Value { get; set; }
    public override GameObject DataValue { get { return SO_Value; } set { SO_Value = value; } }
    public void SetActiveState(bool state)
    {
        Debug.Log($"{SO_Value.gameObject.name}");
        SO_Value.SetActive(state);
    }
}
