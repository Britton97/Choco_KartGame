using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Base : Health_Abstract
{
    public override void ChangeHealth(float changeAmount)
    {
        Debug.Log($"got here and recieved {changeAmount}");
        currentHealth += changeAmount;

        if(currentHealth <= 0)
        {
            onHealthBelowZero.Invoke();
        }
        else
        {
            onHealthChanged.Invoke();
            onHealthChangedFloat.Invoke(currentHealth);
        }

        SendOutHealthPercentage();
    }

    void Start()
    {
        StartEvent();
        SetHealthToMax();
        SendOutMaxHealth();
        //SendOutHealthPercentage();
    }
}
