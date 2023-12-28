using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health_Abstract : MonoBehaviour
{
    [SerializeField] DataFloat maxHealth;
    [SerializeField] protected float currentHealth = 5;

    [SerializeField] private UnityEvent startEvent;
    [SerializeField] protected UnityEvent onHealthChanged;
    [SerializeField] protected UnityEvent<float> onHealthChangedFloat;
    [SerializeField] protected UnityEvent onHealthBelowZero;
    [SerializeField] protected UnityEvent<float> sendOutMaxHealth;
    [SerializeField] private UnityEvent<float> sendOutHealthPercentage;

    public abstract void ChangeHealth(float changeAmount);
    public void SetHealthToMax()
    {
        currentHealth = maxHealth.DataValue;
    }

    public void SendOutMaxHealth()
    {
        sendOutMaxHealth.Invoke(maxHealth.DataValue);
    }

    public float ReturnCurrentHealth()
    {
        return currentHealth;
    }
    public float ReturnMaxHealth()
    {
        return maxHealth.DataValue;
    }

    public void SendOutHealthPercentage()
    {
        sendOutHealthPercentage.Invoke(currentHealth / maxHealth.DataValue);
    }

    public void StartEvent()
    {
        startEvent.Invoke();
    }
}
