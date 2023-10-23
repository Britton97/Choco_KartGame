using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityGetter : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] public DataFloat speed;
    //[SerializeField] public float velocity;

    private void Update()
    {
        //velocity = rb.velocity.magnitude;
        speed.DataValue = rb.velocity.magnitude;
    }
}
