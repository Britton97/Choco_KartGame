using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Checker/ItemBehavior", fileName = "ItemBehavior")]
public class C_ItemBehavior : InterfaceChecker
{
    public override Component CheckInterface(GameObject passIn)
    {
        if (passIn.TryGetComponent<ItemBehavior>(out ItemBehavior item))
        {
            return item as Component;
        }
        return null;
    }
}