using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingGunTrigger : MonoBehaviour
{
    [SerializeField] private GunController gunController;
    [SerializeField] private bool leftController;
    [SerializeField] private bool rightController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftController") && leftController)
        {
            gunController.leftHandOn = true;
            //Debug.Log("Left hand on");
        }
        else if (other.CompareTag("RightController") && rightController)
        {
            gunController.rightHandOn = true;
            //Debug.Log("Right hand on");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftController") && leftController)
        {
            gunController.leftHandOn = false;
            //Debug.Log("Left hand off");
        }
        else if (other.CompareTag("RightController") && rightController)
        {
            gunController.rightHandOn = false;
            //Debug.Log("Left hand off");
        }
    }
}
