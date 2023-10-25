using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    //IMPORTANT!!!!!
    //THIS SCRIPT IS NOT WORKING RIGHT NOW
    //IMPORTANT!!!!!
    [SerializeField] StatType statType;
    [SerializeField] DataItem dataItem;
    [SerializeField] GameObject itemParent;
    [SerializeField] C_CollectItemBeh c_CollectItemBeh;
    [SerializeField] ParticleSystem idle;
    [SerializeField] ParticleSystem burst;
    [SerializeField] FloatReference onCollisionData;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private SphereCollider sphereCollider;
    private GameObject spawnedItem;
    private bool alreadyHit = false;
    private void Start()
    {
        statType = dataItem.ReturnRandomType();
        SpawnCorrectItemMesh();
    }

    private void SpawnCorrectItemMesh()
    {
        //spawn the correct item mesh based on the stat type
        GameObject itemMesh = dataItem.ChooseItemPrefab(statType);
        GameObject item = Instantiate(itemMesh, transform.position, Quaternion.Euler(0,0,25));
        spawnedItem = item;
        item.transform.parent = itemParent.transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) { return; }
        Debug.Log($"{other.gameObject.name} hit me");
        if (c_CollectItemBeh.CheckInterface(other.gameObject) != null)
        {
            idle.Stop();
            burst.Play();
            Debug.Log("hit");
            CollectItemBeh itemCollector = c_CollectItemBeh.CheckInterface(other.gameObject) as CollectItemBeh;
            //itemCollector.PickUpItem(statType, onCollisionData.Value);
            burst.transform.parent = other.transform;
            StartCoroutine(DestroyItem());
            //Destroy(gameObject, 2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            sphereCollider.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private IEnumerator DestroyItem()
    {
        alreadyHit = true;
        spawnedItem.SetActive(false);
        yield return new WaitForSeconds(2);
        Destroy(burst.gameObject);
        Destroy(gameObject);
    }
}
