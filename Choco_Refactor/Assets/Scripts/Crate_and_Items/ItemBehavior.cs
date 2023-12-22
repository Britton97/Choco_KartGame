using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour, ICollisionHandlerable
{
    [SerializeField] StatType statType;

    private bool alreadyHit = false;

    int i = 0;
    public void CollisionHandler(GameObject hitObject, GameObject hittingObject)
    {
        i += 1;
        if (alreadyHit) { return; }
        Debug.Log($"I am {gameObject.name}({i}) / hitObject {hitObject.name} / hittingObject {hittingObject.name}");
        hittingObject.GetComponent<CollectItemBeh>().PickUpItem(statType);
        StartCoroutine(DestroyItem());

    }

    private IEnumerator DestroyItem()
    {
        alreadyHit = true;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
