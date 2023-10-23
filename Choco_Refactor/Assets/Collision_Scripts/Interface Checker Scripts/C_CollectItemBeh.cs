using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Checker/Item Collecter", fileName = "Collector")]
public class C_CollectItemBeh : InterfaceChecker
{
    public override Component CheckInterface(GameObject passIn)
    {
        if (passIn.TryGetComponent<CollectItemBeh>(out CollectItemBeh itemCollector))
        {
            return itemCollector as Component;
        }
        return null;
    }
}
