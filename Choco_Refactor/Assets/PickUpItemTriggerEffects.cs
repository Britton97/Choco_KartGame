using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemTriggerEffects : MonoBehaviour
{
    //[SerializeField] MeshRenderer burstMesh;
    [SerializeField] Material mat;
    [SerializeField] float burstDuration = .5f;
    [SerializeField] AnimationCurve expandCurve;
    [SerializeField] AnimationCurve alphaCurve;


    private void Start()
    {
        InstanceNewMaterial();
        mat.SetFloat("_CircleExpansionPercentage", 0);
        mat.SetFloat("_AlphaPower", 0);
    }

    public void PlayBurstEffect()
    {
        //start a couroutine that will run for the duration of the burst effect

        //if burst effect is already running, stop it and start a new one
        if (isBursting)
        {
            StopCoroutine(BurstEffect());
        }
        StartCoroutine(BurstEffect());
    }

    private WaitForFixedUpdate wait;
    bool isBursting = false;
    private IEnumerator BurstEffect()
    {
        mat.SetFloat("_AlphaPower", 0);
        isBursting = true;
        float timeElspased = 0;

        while(timeElspased < burstDuration)
        {
            Debug.Log("bursting");
            mat.SetFloat("_CircleExpansionPercentage", expandCurve.Evaluate(timeElspased / burstDuration));
            mat.SetFloat("_AlphaPower", alphaCurve.Evaluate(timeElspased / burstDuration));
            timeElspased += Time.fixedDeltaTime;
            yield return wait;
        }
        isBursting = false;
    }

    public void InstanceNewMaterial()
    {
        MeshRenderer burstMesh = GetComponent<MeshRenderer>();
        Material _mat = new Material(burstMesh.material);
        mat = _mat;
        burstMesh.material = _mat;
        //mat = burstMesh.material;
    }
}
