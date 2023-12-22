using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] public InterfaceChecker interfaceChecker;
    [SerializeField] public UnityEvent<GameObject, GameObject> CollisionHandlerEvent;
    [SerializeField] public UnityEvent onPassedInterfaceCheck;


    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log($"I am {gameObject.name} and I collide with {collision.gameObject.name}");
        if(interfaceChecker.CheckInterface(collision.gameObject) != null)
        {
            if(collision.gameObject.GetComponent<ICollisionHandlerable>() != null)
            {
                collision.gameObject.GetComponent<ICollisionHandlerable>().CollisionHandler(this.gameObject, collision.gameObject);
            }
            CollisionHandlerEvent.Invoke(this.gameObject ,collision.gameObject);
            onPassedInterfaceCheck.Invoke();
        }
    }
}