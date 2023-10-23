using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesDamage : MonoBehaviour, IDoDamage
{
    //[SerializeField] private DataFloat damageAmount;
    [SerializeField] private FloatReference damageAmount;
    public float DoDamage()
    {
        return damageAmount.Value;
    }
}