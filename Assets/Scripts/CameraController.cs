using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private CinemachineVirtualCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin m_BasicMultiChannelPerlin;

    private bool isShaking = false;

    private void Awake()
    {
        Instance = this;

        m_BasicMultiChannelPerlin = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeStrong()
    {
        m_BasicMultiChannelPerlin.m_AmplitudeGain = 1f;
        m_BasicMultiChannelPerlin.m_FrequencyGain = .2f;
    }

    public void Shake()
    {
        if (isShaking) return;
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        isShaking = true;
        m_BasicMultiChannelPerlin.m_AmplitudeGain = 3f;
        m_BasicMultiChannelPerlin.m_FrequencyGain = .1f;

        yield return new WaitForSeconds(0.01f);
        
        m_BasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        m_BasicMultiChannelPerlin.m_FrequencyGain = 0f;
        
        transform.rotation = Quaternion.identity;

        isShaking = false;
    }
}
