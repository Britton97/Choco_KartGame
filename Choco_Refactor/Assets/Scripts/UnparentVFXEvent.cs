using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class UnparentVFXEvent : MonoBehaviour
{
    [SerializeField] private VisualEffect vfx;
    [SerializeField] private string eventName;
    [SerializeField] private GameObject myParent;
    [SerializeField] float timeToReparent = 1f;
    [SerializeField] Vector3 parentOffset;
    [SerializeField] Vector3 rotationOffset;

    public void BoostEvent()
    {
        StartCoroutine(ReParent());
        UnparentVFX();
        SendVFXEvent();
    }
    public void UnparentVFX()
    {
        vfx.transform.parent = null;
    }

    [ContextMenu("Call Event")]
    public void SendVFXEvent()
    {
        vfx.SendEvent(eventName);
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
        transform.localPosition = parentOffset;
        transform.localRotation = Quaternion.Euler(rotationOffset);
    }
}
