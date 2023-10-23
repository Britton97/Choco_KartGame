using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class Crate_Beh : MonoBehaviour
{
    private Kart_Input input;
    [SerializeField] private float forceAmount;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float sideForceMax;
    [SerializeField] private float sideForceMin;
    [SerializeField] private float upwardForce;
    [SerializeField] private int health = 3;
    [SerializeField] private GameObject myMesh;
    [SerializeField] private Collider myCollider;
    [SerializeField] private AnimationBehavior animationBehavior;
    [SerializeField] private GameObject itemPrefab;

    #region OnEnable/OnDisable
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    #endregion

    private void Awake()
    {
        input = new Kart_Input();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.velocity = Vector3.zero;
        Vector3 dir = transform.position - collision.transform.position;
        VelocityGetter otherSpeed = collision.transform.GetComponent<VelocityGetter>();
        if (otherSpeed != null)
        {
            dir.Normalize();
            collision.rigidbody.AddForce(-dir * forceAmount * 1000, ForceMode.Impulse);
            TakeDamage(1);
        }
    }

    private void TakeDamage(int passIn)
    {
        health -= passIn;
        if(health == 0)
        {
            HowManyItems();
            //SpawnItemWithForce();
            myCollider.enabled = false;
            myMesh.SetActive(false);
        }
        else if (health >= 1)
        {
            animationBehavior.PlayAnimation();
        }
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
}
