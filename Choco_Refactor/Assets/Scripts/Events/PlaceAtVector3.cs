using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceAtVector3 : MonoBehaviour
{
    public void PlaceAtWorldLocation(Vector3 position)
    {
        transform.position = position;
    }
}
