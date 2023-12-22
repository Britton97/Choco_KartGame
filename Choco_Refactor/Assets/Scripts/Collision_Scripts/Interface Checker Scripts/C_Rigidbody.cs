using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Checker/Rigidbody", fileName = "Rigidbody Checker")]
public class C_Rigidbody : InterfaceChecker
{
    public override Component CheckInterface(GameObject passIn)
    {
        if(passIn.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            return rigidbody as Component;
        }
        return null;
    }
}