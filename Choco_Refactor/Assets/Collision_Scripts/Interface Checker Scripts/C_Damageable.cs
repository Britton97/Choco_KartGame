using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Checker/Damageable", fileName = "Damageable")]
public class C_Damageable : InterfaceChecker
{
    public override Component CheckInterface(GameObject passIn)
    {
        if(passIn.TryGetComponent<IDoDamage>(out IDoDamage damageable))
        {
            return damageable as Component;
        }
        return null;
    }
}
