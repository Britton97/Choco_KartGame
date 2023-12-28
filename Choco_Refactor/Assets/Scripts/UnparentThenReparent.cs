using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnparentThenReparent : MonoBehaviour
{
    [SerializeField] private GameObject myParent;
    [SerializeField] float timeToReparent = 1f;
    [SerializeField] UnityEvent unparentEvent;
    [SerializeField] UnityEvent reparentEvent;

    public void Unparent()
    {
        gameObject.transform.parent = null;
        StartCoroutine(ReParent());
    }

    IEnumerator ReParent()
    {
        float time = 0f;
        while (time < timeToReparent)
        {
            time += Time.deltaTime;
            yield return null;
        }
        transform.parent = myParent.transform;
    }
}
