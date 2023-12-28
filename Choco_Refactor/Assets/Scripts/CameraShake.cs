using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraShake : MonoBehaviour
{
    private CinemachineFreeLook freeLookCam;
    [SerializeField] float shakeIntensity = 1f;
    [SerializeField] float shakeFrequency = 1f;
    [SerializeField] float shakeTime = 1f;

    private void Awake()
    {
        freeLookCam = GetComponent<CinemachineFreeLook>();
    }

    bool alreadyShaking = false;
    public void ShakeCamera()
    {
        if (alreadyShaking)
        {
            return;
        }
        StartCoroutine(ShakeCameraCoroutine());
    }

    private IEnumerator ShakeCameraCoroutine()
    {
        alreadyShaking = true;

        freeLookCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakeIntensity;
        freeLookCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = shakeFrequency;

        yield return new WaitForSeconds(shakeTime);

        freeLookCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        freeLookCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;

        alreadyShaking = false;
    }
}
