using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Platform_Behavior : MonoBehaviour
{
    [SerializeField] List<GameObject> points = new List<GameObject>();
    [SerializeField] private float duration = 5f;
    [SerializeField] private float waitDuration = 5;
    private float elaspedTime;
    private GameObject currentTarget;
    private GameObject lastTarget;
    private int counter = 1;
    [SerializeField] private LayerMask playerLayer;
    private List<GameObject> playersOnPlatform = new List<GameObject>();
    private bool waiting = false;

    private void Start()
    {
        transform.position = points[0].transform.position;
        lastTarget = points[0];
        currentTarget = points[1];
        TurnOffMesh();
    }
    private void FixedUpdate()
    {
        MoveToNextPlatform();
    }

    private void MoveToNextPlatform()
    {
        elaspedTime += Time.deltaTime;
        float percentageComplete = elaspedTime / duration;
        transform.position = Vector3.Lerp(lastTarget.transform.position, currentTarget.transform.position, percentageComplete);

        if(transform.position == currentTarget.transform.position && waiting == false)
        {
            StartCoroutine(WaitAtPoint());
        }
    }

    public IEnumerator WaitAtPoint()
    {
        waiting = true;
        //Debug.Log("Waiting");

        yield return new WaitForSeconds(waitDuration);

        lastTarget = currentTarget;
        if (counter < points.Count - 1)
        {
            counter++;
            currentTarget = points[counter];
        }
        else
        {
            counter = 0;
            currentTarget = points[counter];
        }
        elaspedTime = 0;

        waiting = false;
    }

    public void TurnOffMesh()
    {
        foreach(GameObject point in points)
        {
            point.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        int targetLayer = LayerMask.NameToLayer("Player");
        //if collision with player layer
        if (collision.gameObject.layer == targetLayer)
        {
            //set player's parent to this platform
            playersOnPlatform.Add(collision.gameObject);
            collision.gameObject.transform.root.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //if collision is the playersonplatform list
        if (playersOnPlatform.Contains(collision.gameObject))
        {
            playersOnPlatform.Remove(collision.gameObject);
            collision.gameObject.transform.parent.parent = null;
        }
    }
}
