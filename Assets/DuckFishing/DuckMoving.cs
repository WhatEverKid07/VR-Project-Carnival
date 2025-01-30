using UnityEngine;

public class DuckMoving : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;  // Speed of movement
    [SerializeField] float radius = 0.5f;   // Orbit radius
    [SerializeField] Vector3 centerPoint = Vector3.zero; // Center of movement
    [SerializeField] float startAngle = 0f; // Unique starting angle for each duck

    void Start()
    {
        // Convert start angle to radians
        startAngle = Mathf.Deg2Rad * startAngle;
    }

    void Update()
    {
        float angle = Time.time * (moveSpeed / radius) + startAngle;
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        transform.position = centerPoint + offset;
    }
}
