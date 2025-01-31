using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookHand : MonoBehaviour
{
    //[SerializeField] private HookMe hookMe;
    public ConfigurableJoint joint;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //hookMe = FindAnyObjectByType<HookMe>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftController") || other.CompareTag("RightController"))
        {
            Debug.Log("On Hold!");
            //hookMe.onHold = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftController") || other.CompareTag("RightController"))
        {
            Debug.Log("Off Hold!");
            //hookMe.onHold = false;
        }
    }
}