using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Checker/Trigger Player Pick Up Effects", fileName = "Trigger Player Pick Up Effects")]
public class C_PickUpItemTriggerEffects : InterfaceChecker
{
    public override Component CheckInterface(GameObject passIn)
    {
        if(passIn.TryGetComponent<PickUpItemTriggerEffects>(out PickUpItemTriggerEffects damageable))
        {
            return damageable as Component;
        }
        return null;
    }
}
