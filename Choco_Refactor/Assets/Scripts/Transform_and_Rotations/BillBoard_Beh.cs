using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard_Beh : MonoBehaviour
{
    [SerializeField] private DataGameObject dataGameObject;
    private GameObject lookAtObject;
    void Start()
    {
        Debug.Log($"Biilboard here: {gameObject.name}");
        lookAtObject = dataGameObject.DataValue;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAtObject.transform.position);
    }
}
