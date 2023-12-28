using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMaterialFields : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] string fieldName;
    [SerializeField] string fieldName2;
    [SerializeField] string fieldName3;
    [SerializeField] AnimationCurve curve;

    [SerializeField] float timeToFade = 1f;
    [SerializeField] float healthBarWiggleTime = 1f;


    private void Start()
    {
        material = GetComponent<Image>().material;
    }
    public void SetFloat(float value)
    {
        material.SetFloat(fieldName, value);
        addFade = value;

        StartCoroutine(FadeBackToZero());
        StartCoroutine(HealthBarWiggle());
    }

    public void SetStartValue()
    {
        material.SetFloat(fieldName, 1f);
        material.SetFloat(fieldName2, 0);
    }

    float addFade;
    private IEnumerator FadeBackToZero()
    {
        float elapsedTime = 0f;
        float startValue = addFade;

        while (elapsedTime < timeToFade)
        {
            material.SetFloat(fieldName2, Mathf.Lerp(startValue, 0, (elapsedTime / timeToFade)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //material.SetFloat(fieldName2, endValue);
    }

    private IEnumerator HealthBarWiggle()
    {
        float elapsedTime = 0f;
        float startValue = 0f;
        float endValue = 1f;

        while (elapsedTime < healthBarWiggleTime)
        {
            material.SetFloat(fieldName3, Mathf.Lerp(startValue, endValue, curve.Evaluate(elapsedTime / healthBarWiggleTime)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        material.SetFloat(fieldName3, .5f);
    }
}
