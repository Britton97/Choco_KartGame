using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Checker/Kart", fileName = "Kart Checker")]
public class C_KartChecker : InterfaceChecker
{
    public override Component CheckInterface(GameObject passIn)
    {
        if (passIn.TryGetComponent<KartTag>(out KartTag kartTag))
        {
            return kartTag as Component;
        }

        return null;
    }
}
