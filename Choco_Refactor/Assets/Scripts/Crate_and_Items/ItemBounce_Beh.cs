using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBounce_Beh : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float curveMultiplier = 1;
    [SerializeField] float animationDuration;
    private Vector3 startingPos;
    float elapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //loop the animaiton curve over time. when it reaches the end, it will start over
        elapsedTime += Time.deltaTime;
        if(elapsedTime > animationDuration) { elapsedTime = 0f; }
        float curveValue = curve.Evaluate(elapsedTime/animationDuration);
        transform.localPosition = new Vector3(transform.localPosition.x, startingPos.y + (curveValue * curveMultiplier), transform.localPosition.z);
    }
}
