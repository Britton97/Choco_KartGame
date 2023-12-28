using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] Material hitEffect;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration = 0.5f;
    [SerializeField] float rotateSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        hitEffect = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //if space is pressed start couroutine
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(HitEffectCoroutine());
        }
    }

    //hit effect coroutine
    IEnumerator HitEffectCoroutine()
    {
        hitEffect.SetFloat("_Step", 0);
        float time = 0;
        //while time is less than duration
        while (time < duration)
        {
            //set material float to curve value
            hitEffect.SetFloat("_Step", time / duration);
            //rotate object on local z axis
            transform.Rotate(Vector3.forward * rotateSpeed * curve.Evaluate(time / duration) * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
