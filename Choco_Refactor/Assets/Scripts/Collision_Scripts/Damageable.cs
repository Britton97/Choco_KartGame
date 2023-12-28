using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Health_Abstract))]
//and require component of health abstract
public class Damageable : MonoBehaviour, IDamageable, ICollisionHandlerable
{
    private Health_Abstract healthComponent;

    private void Start()
    {
        healthComponent = GetComponent<Health_Abstract>();
    }
    public void TakeDamage(float damage)
    {
        healthComponent.ChangeHealth(-damage);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void CollisionHandler(GameObject hitObject, GameObject hittingObject)
    {
        Debug.Log($"I am {gameObject.name} / hitObj = {hitObject.name} / hittingObj = {hittingObject.name}");

        DoesDamage_Abstract doesDamage = hittingObject.GetComponent<DoesDamage_Abstract>();
        if (doesDamage != null)
        {
            float _out = doesDamage.DoDamage();
            Debug.Log($"Damage recieved = {_out}");
            TakeDamage(_out);
        }
    }
}