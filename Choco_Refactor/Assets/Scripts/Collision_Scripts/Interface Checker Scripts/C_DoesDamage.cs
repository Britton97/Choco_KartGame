using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Checker/Do Damage", fileName = "Do Damage Checker")]
public class C_DoesDamage : InterfaceChecker
{
    public override Component CheckInterface(GameObject passIn)
    {
        if(passIn.TryGetComponent<DoesDamage_Abstract>(out DoesDamage_Abstract damageable))
        {
            return damageable as Component;
        }
        return null;
    }
}
