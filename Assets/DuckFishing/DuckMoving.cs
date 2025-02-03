using UnityEngine;
using System.Collections;

public class DuckMoving : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Vector3 centerPoint = Vector3.zero;
    [SerializeField] float bounceAngleChange = 15f;

    private Rigidbody rb;
    private float radius;
    private float startAngle;

    void Start()
    {
        moveSpeed = Random.Range(0.01f, 0.1f);
        gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 350), 0);
        Vector3 offset = transform.position - centerPoint;
        radius = offset.magnitude;
        startAngle = Mathf.Atan2(offset.z, offset.x);

        rb = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();

        rb.useGravity = false;
        rb.isKinematic = false;
        rb.mass = 0.5f;
        rb.drag = 2f;
        rb.angularDrag = 2f;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

    void FixedUpdate()
    {
        float angle = Time.time * (moveSpeed / radius) + startAngle;
        Vector3 targetPosition = centerPoint + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        rb.MovePosition(targetPosition);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Duck"))
        {
            float direction = Random.value > 0.5f ? 1f : -1f;
            StartCoroutine(SmoothAngleAdjustment(direction));
        }
    }

    IEnumerator SmoothAngleAdjustment(float direction)
    {
        float step = Mathf.Deg2Rad * bounceAngleChange * direction / 10f;
        for (int i = 0; i < 10; i++)
        {
            startAngle += step;
            yield return new WaitForFixedUpdate(); // Wait for physics update
        }
    }
}