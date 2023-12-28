using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Health_Abstract))]
public class Crate_Beh : MonoBehaviour, ICollisionHandlerable, Health_Interface
{
    private Health_Abstract healthComponent;
    [Header("Knock Back Settings")]
    [SerializeField] private float forceAmount;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float sideForceMax;
    [SerializeField] private float sideForceMin;
    [SerializeField] private float upwardForce;

    [Header("Item Prefab")]
    [SerializeField] private GameObject itemPrefab;
    [Header("Check Collision for Rigidbody")]
    [SerializeField] private InterfaceChecker rigidbodyChecker;

    public float MaxHealth { get => healthComponent.ReturnMaxHealth(); }
    public float CurrentHealth { get => healthComponent.ReturnCurrentHealth();}

    private void Start()
    {
        healthComponent = GetComponent<Health_Abstract>();
    }

    public void SpawnItemPrefabs()
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

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void CollisionHandler(GameObject hitObject, GameObject hittingObject)
    {
        Debug.Log($"I am {gameObject.name} / {hittingObject.gameObject.name} hittingObject / {hitObject.gameObject.name}");

        if(rigidbodyChecker.CheckInterface(hittingObject) == true) { PushBack(hittingObject); }
    }

    private Rigidbody hitRb;
    public void PushBack(GameObject collision)
    {
        if (collision.GetComponent<Rigidbody>() == null)
        {
            return;
        }
        hitRb = collision.GetComponent<Rigidbody>();

        hitRb.velocity = Vector3.zero;

        Vector3 dir = transform.position - collision.transform.position;

        dir.Normalize();
        hitRb.AddForce(-dir * forceAmount * 1000, ForceMode.Impulse);

        hitRb = null;
    }
}
