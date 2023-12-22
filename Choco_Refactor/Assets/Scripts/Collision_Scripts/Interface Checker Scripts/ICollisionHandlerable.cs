using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionHandlerable
{
    public void CollisionHandler(GameObject hitObject, GameObject hittingObject);
}
