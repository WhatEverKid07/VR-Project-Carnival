using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DartThrow : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    public float throwForce = 15f; // Adjust to control speed
    public bool canThrow;

    private void Awake()
    {
        canThrow = false;
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        //rb.isKinematic = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftController") || other.CompareTag("RightController"))
        {
            canThrow = true;
        }
    }
    private void Update()
    {
        if (canThrow)
        {
            OnThrow();
        }
    }


    private void OnThrow()
    {
        //rb.isKinematic = false;
        rb.useGravity = true;

        // Apply velocity in the correct forward direction
        rb.velocity = -transform.forward * throwForce;
    }
}
