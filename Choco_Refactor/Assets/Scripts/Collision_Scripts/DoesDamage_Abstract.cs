using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health_Abstract))]
public abstract class DoesDamage_Abstract : MonoBehaviour, IDoDamage
{
    public abstract float DoDamage();
}