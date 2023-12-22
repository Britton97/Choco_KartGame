using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetPlayerCamera : MonoBehaviour
{
    [SerializeField] PlayerInput player;
    [ContextMenu("SetCam")]
    public void SetCam()
    {
        //gameObject.layer = LayerMask.NameToLayer($"P{playerCount.DataValue} Camera");
        //set the gameobject's layer to int 3

    }
}
