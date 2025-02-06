using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Mathematics;

public class VRMovement : MonoBehaviour
{
    [Header("Spline & Player Settings")]
    public SplineContainer splineContainer; // Reference to the SplineContainer
    public Transform player; // Player transform (headset/camera root)

    [Header("Movement Settings")]
    public float moveSpeed = 2f; // Speed at which player moves to the spline
    public float followSpeed = 4f; // Speed while following the spline
    public float stopDistance = 0.1f; // When to start following the spline
    public float rotationSpeed = 5f; // Speed of rotation towards spline direction

    [Header("Fade & Scene Transition")]
    public string nextSceneName = "NextScene"; // Scene to load
    public CanvasGroup fadeCanvas;

    private bool movingToSpline = false;
    private bool followingSpline = false;
    private float t = 0f; // Spline progress parameter
    private float lerpProgress = 0f; // Smooth transition progress
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 nearestPointOnSpline; // Stores nearest position on spline
    private Spline nearestSpline; // Stores reference to the nearest spline

    
    private void Start()
    {
        StartTransport();
    }
    

    public void StartTransport()
    {
        Debug.Log("Finding Nearest Spline...");

        // Null check to avoid crashes
        if (splineContainer == null || player == null || splineContainer.Splines.Count == 0)
        {
            Debug.LogError("SplineContainer or Player is not assigned, or there are no splines!");
            return;
        }

        // Variables to track the closest spline and point
        float closestDistance = float.MaxValue;
        float nearestT = 0f;
        float3 nearestFloat3 = float3.zero;

        // 🔍 Loop through all splines to find the closest one
        foreach (Spline s in splineContainer.Splines)
        {
            float3 tempNearestPoint;
            float tempT;
            SplineUtility.GetNearestPoint(s, splineContainer.transform.InverseTransformPoint(player.position), out tempNearestPoint, out tempT); // Convert player position to local space

            // Convert tempNearestPoint to world space before calculating distance
            Vector3 worldNearestPoint = splineContainer.transform.TransformPoint((Vector3)tempNearestPoint);
            float distance = Vector3.Distance(player.position, worldNearestPoint);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestFloat3 = tempNearestPoint;
                nearestT = tempT;
                nearestSpline = s; // Store reference to the closest spline
            }
        }

        if (nearestSpline == null)
        {
            Debug.LogError("No valid spline found!");
            return;
        }

        // Convert nearest spline point to world space
        nearestPointOnSpline = splineContainer.transform.TransformPoint((Vector3)nearestFloat3);

        // 🔧 Ensure Y-axis stays the same to prevent "falling to the floor"
        nearestPointOnSpline.y = player.position.y;

        movingToSpline = true;
        followingSpline = false;
        t = nearestT;

        // Store initial position and rotation
        initialPosition = player.position;
        initialRotation = player.rotation;
        lerpProgress = 0f;
    }

    private void Update()
    {
        if (movingToSpline)
        {
            MoveToSpline();
        }
        else if (followingSpline)
        {
            FollowSpline();
        }
    }

    private void MoveToSpline()
    {
        lerpProgress += Time.deltaTime * moveSpeed;

        // 🔧 Keep the Y-axis constant to prevent the player from rushing to the floor
        Vector3 newPosition = Vector3.Lerp(initialPosition, nearestPointOnSpline, lerpProgress);
        newPosition.y = initialPosition.y; // Keep original Y position

        player.position = newPosition;

        // Smoothly rotate towards spline
        Vector3 directionToSpline = (nearestPointOnSpline - initialPosition).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToSpline, Vector3.up);
        player.rotation = Quaternion.Slerp(initialRotation, targetRotation, lerpProgress * rotationSpeed);

        // Check if player is close enough to start following the spline
        if (Vector3.Distance(player.position, nearestPointOnSpline) < stopDistance)
        {
            movingToSpline = false;
            followingSpline = true;
        }
    }

    private void FollowSpline()
    {
        t += (followSpeed / nearestSpline.GetLength()) * Time.deltaTime; // Normalised speed
        t = Mathf.Clamp01(t); // Ensure t stays within 0 - 1 range

        if (t >= 1f)
        {
            StartCoroutine(FadeOutAndChangeScene());
        }
        else
        {
            // Convert spline position from local to world space
            player.position = splineContainer.transform.TransformPoint(SplineUtility.EvaluatePosition(nearestSpline, t));

            // Rotate player along the spline direction smoothly
            Quaternion splineRotation = Quaternion.LookRotation(SplineUtility.EvaluateTangent(nearestSpline, t), Vector3.up);
            player.rotation = Quaternion.RotateTowards(player.rotation, splineRotation, rotationSpeed * Time.deltaTime * 100f);
        }
    }

    private IEnumerator FadeOutAndChangeScene()
    {
        float fadeDuration = 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            fadeCanvas.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
