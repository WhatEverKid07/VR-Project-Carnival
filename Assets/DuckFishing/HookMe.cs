using UnityEngine;

public class HookMe : MonoBehaviour
{
    private ConfigurableJoint currentJoint;
    private GameObject hookedDuck;
    private Rigidbody duckRb;

    private void OnTriggerEnter(Collider other)
    {
        // Ensure only one duck can be hooked at a time
        if (hookedDuck != null) return;

        if (other.CompareTag("DuckHook"))
        {
            Transform duckParent = other.transform.parent; // Get parent duck object
            Rigidbody newDuckRb = duckParent.GetComponent<Rigidbody>();

            if (newDuckRb != null)
            {
                hookedDuck = newDuckRb.gameObject;
                duckRb = newDuckRb;

                // Find and destroy the DuckMoving script
                DuckMoving duckMoving = hookedDuck.GetComponent<DuckMoving>();
                if (duckMoving != null)
                {
                    Destroy(duckMoving);
                }

                duckRb.interpolation = RigidbodyInterpolation.Interpolate;
                duckRb.useGravity = false;
                duckRb.velocity = Vector3.zero;
                duckRb.angularVelocity = Vector3.zero;
                duckRb.constraints = RigidbodyConstraints.None;

                StartCoroutine(SmoothAttach(hookedDuck.transform, transform.position, transform.rotation));

                // Attach with a properly configured ConfigurableJoint
                currentJoint = gameObject.AddComponent<ConfigurableJoint>();
                currentJoint.connectedBody = duckRb;

                // Prevent extreme coordinate values
                currentJoint.autoConfigureConnectedAnchor = true;

                // Lock position but allow slight rotation
                currentJoint.xMotion = ConfigurableJointMotion.Locked;
                currentJoint.yMotion = ConfigurableJointMotion.Locked;
                currentJoint.zMotion = ConfigurableJointMotion.Locked;

                currentJoint.angularXMotion = ConfigurableJointMotion.Limited;
                currentJoint.angularYMotion = ConfigurableJointMotion.Limited;
                currentJoint.angularZMotion = ConfigurableJointMotion.Limited;

                // Apply soft joint limits
                SoftJointLimit limit = new SoftJointLimit { limit = 15f };
                currentJoint.lowAngularXLimit = limit;
                currentJoint.highAngularXLimit = limit;
                currentJoint.angularYLimit = limit;
                currentJoint.angularZLimit = limit;

                // Prevent physics explosion
                currentJoint.projectionMode = JointProjectionMode.PositionAndRotation;
                currentJoint.enablePreprocessing = false;
            }
        }
    }

    private System.Collections.IEnumerator SmoothAttach(Transform duckTransform, Vector3 targetPosition, Quaternion targetRotation)
    {
        float duration = 0.2f;
        float elapsed = 0f;
        Vector3 startPosition = duckTransform.position;
        Quaternion startRotation = duckTransform.rotation;

        while (elapsed < duration)
        {
            duckTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            duckTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (float.IsNaN(targetPosition.x) || float.IsInfinity(targetPosition.x))
        {
            targetPosition = Vector3.zero;
        }
        if (float.IsNaN(targetRotation.x) || float.IsInfinity(targetRotation.x))
        {
            targetRotation = Quaternion.identity;
        }

        duckTransform.position = targetPosition;
        duckTransform.rotation = targetRotation;
    }

    private void Update()
    {
        if (hookedDuck != null && Input.GetKeyDown(KeyCode.Space))
        {
            ReleaseDuck();
        }
    }

    private void ReleaseDuck()
    {
        if (currentJoint != null)
        {
            Destroy(currentJoint);
            currentJoint = null;
            duckRb.useGravity = true;
            hookedDuck = null;
            duckRb = null;
        }
    }
}
