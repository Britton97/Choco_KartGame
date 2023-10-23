using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchParentBehavior : MonoBehaviour, ICollisionHandlerable
{

    public void SwitchParent(Transform newParent)
    {
        transform.parent = newParent;
    }

    public void SwitchToWorldParent()
    {
        transform.parent = null;
    }
    public void CollisionHandler(GameObject hitObject, GameObject hittingObject)
    {
        if(hitObject.transform.parent == null) { return; }

        hittingObject.transform.parent = hitObject.transform.parent;
    }

}
