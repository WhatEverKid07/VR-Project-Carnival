using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class VRMovement : MonoBehaviour
{
    [Header("Spline & Player Settings")]
    public SplineContainer spline; // Reference to the spline
    public Transform player; // Player transform (headset/camera root)

    [Header("Movement Settings")]
    public float moveSpeed = 2f; // Speed at which player moves to the spline
    public float followSpeed = 4f; // Speed while following the spline
    public float stopDistance = 0.1f; // When to start following the spline
    public float rotationSpeed = 5f; // Speed of rotation towards spline direction

    [Header("Fade & Scene Transition")]
    public CanvasGroup fadeCanvas; // UI Canvas Group for fade effect
    public string nextSceneName = "NextScene"; // Scene to load

    //private SplineUtility.SplineToNativePoint _nearestPoint; // look up!!!!
    private bool movingToSpline = false;
    private bool followingSpline = false;
    private float t = 0f; // Spline progress parameter
    private float lerpProgress = 0f; // Smooth transition progress
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    public void StartTransport()
    {
        // Null check to avoid crashes
        if (spline == null || player == null)
        {
            Debug.LogError("Spline or Player is not assigned!");
            return;
        }

        // Get nearest spline point
        //_nearestPoint = SplineUtility.GetNearestPoint(spline.Splines[0], player.position, out float nearestT); // Look Up !!!!
        movingToSpline = true;
        followingSpline = false;
        //t = nearestT;

        // Store initial position and rotation
        initialPosition = player.position;
        initialRotation = player.rotation;
        lerpProgress = 0f;
    }

    private void Update()
    {
        if (movingToSpline)
        {
            //MoveToSpline();
        }
        else if (followingSpline)
        {
            FollowSpline();
        }
    }
    /*
    private void MoveToSpline()
    {
        lerpProgress += Time.deltaTime * moveSpeed;
        player.position = Vector3.Lerp(initialPosition, _nearestPoint.Position, lerpProgress);

        // Smoothly rotate towards spline
        Quaternion targetRotation = Quaternion.LookRotation(_nearestPoint.Tangent, Vector3.up);
        player.rotation = Quaternion.Slerp(initialRotation, targetRotation, lerpProgress * rotationSpeed);

        // Check if player is close enough to start following the spline
        if (Vector3.Distance(player.position, _nearestPoint.Position) < stopDistance)
        {
            movingToSpline = false;
            followingSpline = true;
        }
    }
    */

    private void FollowSpline()
    {
        t += (followSpeed / spline.Splines[0].GetLength()) * Time.deltaTime; // Normalised speed
        t = Mathf.Clamp01(t); // Ensure t stays within 0 - 1 range

        if (t >= 1f)
        {
            StartCoroutine(FadeOutAndChangeScene());
        }
        else
        {
            player.position = spline.EvaluatePosition(t);

            // Rotate player along the spline direction smoothly
            Quaternion splineRotation = Quaternion.LookRotation(spline.EvaluateTangent(t), Vector3.up);
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
