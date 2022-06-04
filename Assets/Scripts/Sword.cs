using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private CinemachineShake cinemachineShake;

    private void Start()
    {
        cinemachineShake = CinemachineShake.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("There was a sword trigger: " + other.gameObject.name);
        if (other.gameObject.GetComponent<Hammer>() != null)
        {
            Debug.Log("Hammer hit sword!");
        }
    }
}
