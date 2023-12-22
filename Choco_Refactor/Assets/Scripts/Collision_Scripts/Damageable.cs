using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Damageable : MonoBehaviour, IDamageable
{
    [Header("Health Fields")]
    [SerializeField] private DataFloat maxHealth;
    [SerializeField] private float health;
    [SerializeField] InterfaceChecker iDoDamage;
    public FloatReference healthReference;

    [Header("On Damage Events")]
    [SerializeField] private UnityEvent<float> updateHealth;
    [SerializeField] private UnityEvent onHealthBelowZero;

    private void Start()
    {
        health = maxHealth.DataValue;
        updateHealth.Invoke(health);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        updateHealth.Invoke(health);
        if (health <= 0)
        {
            onHealthBelowZero.Invoke();
            Debug.Log("I'm dead");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(iDoDamage.CheckInterface(other.gameObject) != null)
        {
            float damageAmount = other.GetComponent<IDoDamage>().DoDamage();
            healthReference.dataEvent.Invoke(damageAmount);

            //somehow i need to send information back to the collider that hit me
            //so that i can give it points for hitting me
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}