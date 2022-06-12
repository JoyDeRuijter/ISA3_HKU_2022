using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOn : MonoBehaviour
{
    [SerializeField] Light[] lights;
    [SerializeField] LayerMask lightMaskOn;
    [SerializeField] LayerMask lightMaskOff;
    private bool isOn;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("pressed space");
            if (isOn)
                TurnOffLights();
            else
                TurnOnLights();
        }
    }

    void TurnOnLights()
    {
        foreach (Light light in lights)
        {
            light.cullingMask = lightMaskOn;
        }
        isOn = true;
    }

    void TurnOffLights()
    {
        foreach (Light light in lights)
        {
            light.cullingMask = lightMaskOff;
        }
        isOn = false;
    }
}
