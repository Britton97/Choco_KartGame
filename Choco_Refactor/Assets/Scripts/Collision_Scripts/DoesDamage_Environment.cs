using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DoesDamage_Environment : DoesDamage_Abstract
{
    [SerializeField] public float environmentDamage;
    public override float DoDamage()
    {
        return environmentDamage;
    }
}
