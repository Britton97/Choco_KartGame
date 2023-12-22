using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Checker/Collect Item", fileName = "Collect Item Checker")]
public class C_CollectItem_Beh : InterfaceChecker
{
    public override Component CheckInterface(GameObject passIn)
    {
        if (passIn.TryGetComponent<CollectItemBeh>(out CollectItemBeh collectItemBeh))
        {
            return collectItemBeh as Component;
        }
        return null;
    }
}