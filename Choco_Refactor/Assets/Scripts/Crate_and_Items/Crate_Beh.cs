using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.ShaderData;

public class Crate_Beh : MonoBehaviour, IDamageable, ICollisionHandlerable
{
    [SerializeField] private float forceAmount;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float sideForceMax;
    [SerializeField] private float sideForceMin;
    [SerializeField] private float upwardForce;
    [SerializeField] private DataFloat maxHealth;
    [SerializeField] private float health = 3;
    [SerializeField] private GameObject myMesh;
    [SerializeField] private Collider myCollider;
    [SerializeField] private AnimationBehavior animationBehavior;
    [SerializeField] private GameObject itemPrefab;

    private void Start()
    {
        health = maxHealth.DataValue;
    }

    public void HowManyItems()
    {
        int amount = Random.Range(1, 4);
        for (int i = 0; i < amount; i++)
        {
            SpawnItemWithForce();
        }
    }

    public void SpawnItemWithForce()
    {
        GameObject spawn = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = spawn.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speedMultiplier, ForceMode.Impulse);

        float x = Random.Range(-1, 1);
        float z = Random.Range(-1, 1);
        Vector2 dir = new Vector2(x, z);
        dir = dir.normalized;

        float randomSideForce = Random.Range(sideForceMin, sideForceMax);

        rb.AddForce(new Vector3(dir.x * randomSideForce, upwardForce, dir.y * randomSideForce), ForceMode.Impulse);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"I took {damage} damage");
        health -= damage;
        if (health <= 0)
        {
            HowManyItems();
            //SpawnItemWithForce();
            myCollider.enabled = false;
            myMesh.SetActive(false);
        }
        else if (health > 0)
        {
            animationBehavior.PlayAnimation();
        }
    }
    public void CollisionHandler(GameObject hitObject, GameObject hittingObject)
    {
        DoesDamage doesDamage = hittingObject.GetComponent<DoesDamage>();
        //VelocityGetter velocityGetter = hittingObject.GetComponent<VelocityGetter>();
        //eventually I would like it to take into account the velocity of the hitting object along with the offense amount
        Debug.Log($"I am {gameObject.name} / {hittingObject.gameObject.name} hittingObject / {hitObject.gameObject.name}");
        //Debug.Log($"I am  {gameObject.name} / {hittingObject.gameObject.name} hitObject");
        TakeDamage(doesDamage.DoDamage());
        if (hittingObject.GetComponent<Rigidbody>() == null)
        {
            return;
        }
        PushBack(hittingObject);
    }

    private Rigidbody hitRb;
    public void PushBack(GameObject collision)
    {
        //if collision object does not have a rigidbody, return
        if (collision.GetComponent<Rigidbody>() == null)
        {
            return;
        }
        hitRb = collision.GetComponent<Rigidbody>();
        //Vector3 currentSpeed = hitRb.velocity;
        hitRb.velocity = Vector3.zero;

        Vector3 dir = transform.position - collision.transform.position;

        dir.Normalize();
        hitRb.AddForce(-dir * forceAmount * 1000, ForceMode.Impulse);
        //TakeDamage(1);


        hitRb = null;
    }
}
