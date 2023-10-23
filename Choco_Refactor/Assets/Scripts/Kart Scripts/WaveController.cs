using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WaveController : MonoBehaviour
{
    [SerializeField] float waveLength = 1;
    [SerializeField] Vector3 leftWaveOffset = new Vector3(0,1,-1);
    [SerializeField] Vector3 rightWaveOffset = new Vector3(0,1,-1);
    [SerializeField] List<GameObject> waves = new List<GameObject>();
    [SerializeField] Material leftWaveMaterial;
    [SerializeField] Material rightWaveMaterial;
    [SerializeField] UnityEvent<Vector2> vector2Event;
    [HideInInspector] public KartController kartController;
    [Range(0,1)]
    [SerializeField] float minStickThreshold = 0f;

    [SerializeField] float degradeRate;
    [SerializeField] float maxRotateAmount;
    [SerializeField] float rotAmount;
    public float WaveLength
    {
        get { return waveLength; }
        set
        {
            if(value != waveLength)
            {
                waveLength = value;
                ChangeWaveLength();
            }
        }
    }

    private void OnValidate()
    {
        ChangeWaveLength();
    }

    private void FixedUpdate()
    {
        //DegradeWaves();
    }

    public void WaveRotation(float pInput)
    {
        if (Mathf.Abs(pInput) * maxRotateAmount > rotAmount)
        {
            rotAmount = maxRotateAmount * pInput;
        }

        transform.localRotation = Quaternion.Euler(0, rotAmount, 0);
        rotAmount -= degradeRate * Time.deltaTime;
    }

    public void ReceiveVector2Input(Vector2 pInput)
    {
        //Debug.Log(pInput);
        ChangeWaveOffset(pInput.x);
        WaveRotation(pInput.x);
    }

    private void ChangeWaveLength()
    {
        foreach (GameObject wave in waves)
        {
            Vector3 newScale = new Vector3(wave.transform.localScale.x, wave.transform.localScale.y, waveLength);
            wave.transform.localScale = newScale;
        }
    }

    [SerializeField] float maxWaveAmount;
    [SerializeField] float minWaveAmount;
    [SerializeField] float domWaveAmount;
    [SerializeField] float subWaveAmount;
    [SerializeField] Vector3 domWaveOffset;
    [SerializeField] Vector3 subWaveOffset;
    private Vector3 neutralOffset = new Vector3(0, 1, -1);

    private void ChangeWaveOffset(float pInput)
    {
        float inputAbs = Mathf.Abs(pInput);
        if (inputAbs >= minStickThreshold)
        {
            if (inputAbs * maxWaveAmount > domWaveAmount)
            {
                domWaveAmount = maxWaveAmount * Mathf.Abs(pInput);
                subWaveAmount = subWaveAmount * Mathf.Abs(pInput);

            }
            if (pInput > 0)
            {
                //left wave dominant
                leftWaveMaterial.SetVector("_DirectionalInfluence", Vector3.Lerp(leftWaveMaterial.GetVector("_DirectionalInfluence"), domWaveOffset, Time.deltaTime * degradeRate));
                rightWaveMaterial.SetVector("_DirectionalInfluence", Vector3.Lerp(rightWaveMaterial.GetVector("_DirectionalInfluence"), subWaveOffset, Time.deltaTime * degradeRate));
            }
            else
            {
                //right wave dominant
                leftWaveMaterial.SetVector("_DirectionalInfluence", Vector3.Lerp(leftWaveMaterial.GetVector("_DirectionalInfluence"), subWaveOffset, Time.deltaTime * degradeRate));
                rightWaveMaterial.SetVector("_DirectionalInfluence", Vector3.Lerp(rightWaveMaterial.GetVector("_DirectionalInfluence"), domWaveOffset, Time.deltaTime * degradeRate));
            }
        }
        else
        {
            Debug.Log("Degrading");
            DegradeWaves();
        }
    }

    private void DegradeWaves()
    {
        leftWaveMaterial.SetVector("_DirectionalInfluence", Vector3.Lerp(leftWaveMaterial.GetVector("_DirectionalInfluence"), neutralOffset, Time.deltaTime * degradeRate));
        rightWaveMaterial.SetVector("_DirectionalInfluence", Vector3.Lerp(rightWaveMaterial.GetVector("_DirectionalInfluence"), neutralOffset, Time.deltaTime * degradeRate));
    }
}
