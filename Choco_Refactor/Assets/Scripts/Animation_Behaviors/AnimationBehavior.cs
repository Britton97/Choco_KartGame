using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBehavior : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float animationDuration;
    private WaitForFixedUpdate waitForFixedUpdate;
    private bool isPlaying = false;
    private void Awake()
    {
        waitForFixedUpdate = new WaitForFixedUpdate();
    }

    public void PlayAnimation()
    {
        if(isPlaying) { return; }
        StartCoroutine(AnimationCoroutine());
    }

    private IEnumerator AnimationCoroutine()
    {
        isPlaying = true;
        float startTime = Time.time;
        float endTime = startTime + animationDuration;
        Vector3 startPosition = transform.position;
        while (Time.time < endTime)
        {
            float timeSinceStarted = Time.time - startTime;
            float percentageComplete = timeSinceStarted / animationDuration;
            float curveValue = curve.Evaluate(percentageComplete);
            transform.position = startPosition + new Vector3(0, curveValue, 0);
            yield return waitForFixedUpdate;
        }
        isPlaying = false;
    }
}
