using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    [SerializeField] private Animator anim;
    [SerializeField] private float intensity;
    [SerializeField] private float shakeTime;
    private CinemachineVirtualCamera cinemachineVC;
    private CinemachineBasicMultiChannelPerlin cinemachineBMCP;
    private bool isShaking;

    private void Awake()
    {
        Instance = this;
        cinemachineVC = GetComponent<CinemachineVirtualCamera>();
        cinemachineBMCP = cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(StartShaking(2.2f));
            StartCoroutine(StopShaking());
        }

        if (isShaking)
            cinemachineBMCP.m_AmplitudeGain = intensity;
        else
            cinemachineBMCP.m_AmplitudeGain = 0f;
    }

    public IEnumerator StopShaking()
    {
        yield return new WaitForSeconds(shakeTime);
        anim.SetBool("HasClicked", false);
        isShaking = false;
    }

    public IEnumerator StartShaking(float inBetweenTime)
    {
        anim.SetBool("HasClicked", true);
        yield return new WaitForSeconds(inBetweenTime);
        isShaking = true;
        anim.SetBool("HasClicked", false);
    }
}
