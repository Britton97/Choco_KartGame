using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Checker/Player", fileName = "Player Checker")]
public class C_PlayerChecker : InterfaceChecker
{
    public override Component CheckInterface(GameObject passIn)
    {
        if (passIn.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            return playerController as Component;
        }
        return null;
    }
}
