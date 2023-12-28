using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField] public InterfaceChecker interfaceChecker;
    [SerializeField] public UnityEvent<GameObject, GameObject> CollisionHandlerEvent;
    [SerializeField] public UnityEvent onPassedInterfaceCheck;

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log($"Collision with {collision.gameObject.name}");
        if (interfaceChecker.CheckInterface(collision.gameObject) != null)
        {
            if (collision.gameObject.GetComponent<ICollisionHandlerable>() != null)
            {
                //collision.gameObject.GetComponent<ICollisionHandlerable>().CollisionHandler(this.gameObject, collision.gameObject);
            }
            CollisionHandlerEvent.Invoke(this.gameObject, collision.gameObject);
            onPassedInterfaceCheck.Invoke();
        }
    }
}
