using UnityEngine;

public class HookPickupWithJoint : MonoBehaviour
{
    private FixedJoint currentJoint;
    private GameObject hookedDuck;

    private void OnTriggerEnter(Collider other)
    {
        if (currentJoint == null && other.CompareTag("DuckHook")) // Ensure the duck has this tag
        {
            Rigidbody duckRb = other.transform.parent.GetComponent<Rigidbody>(); // Get the duck's Rigidbody

            if (duckRb != null)
            {
                hookedDuck = duckRb.gameObject;

                // Align positions BEFORE attaching to prevent sudden snapping issues
                hookedDuck.transform.position = transform.position;
                hookedDuck.transform.rotation = transform.rotation;

                // Stop any movement to prevent physics jitter
                duckRb.velocity = Vector3.zero;
                duckRb.angularVelocity = Vector3.zero;

                // Attach with FixedJoint
                currentJoint = gameObject.AddComponent<FixedJoint>();
                currentJoint.connectedBody = duckRb;
                currentJoint.breakForce = Mathf.Infinity; // Prevent accidental breaking
                currentJoint.breakTorque = Mathf.Infinity;
            }
        }
    }

    private void Update()
    {
        if (hookedDuck != null && Input.GetKeyDown(KeyCode.Space)) // Replace with VR input
        {
            ReleaseDuck();
        }
    }

    private void ReleaseDuck()
    {
        if (currentJoint != null)
        {
            Destroy(currentJoint); // Remove the joint
            currentJoint = null;

            Rigidbody rb = hookedDuck.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 2f, ForceMode.Impulse); // Apply force on release
            hookedDuck = null;
        }
    }
}
