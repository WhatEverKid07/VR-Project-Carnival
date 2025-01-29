using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DartThrow : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private Vector3 lastPosition;
    private Vector3 throwVelocity;
    private bool isGrabbed = false;
    private float velocityMultiplier = 8000000f; // Adjust if needed

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Listen for grab and release events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void FixedUpdate()
    {
        if (isGrabbed)
        {
            // Calculate velocity while held
            throwVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
            lastPosition = transform.position;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        rb.isKinematic = true; // Disable physics while holding
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        rb.isKinematic = false; // Enable physics
        rb.velocity = throwVelocity * velocityMultiplier; // Apply velocity on release

        Debug.Log("Dart Released! Velocity: " + rb.velocity);
    }
}
