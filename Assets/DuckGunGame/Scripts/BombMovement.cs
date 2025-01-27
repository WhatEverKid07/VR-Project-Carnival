using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public List<Transform> targetPoints;
    public float moveSpeed;
    public int pauseTimer;

    private int currentTargetIndex = 0;
    private bool pause;

    private void Start()
    {
        pause = true;
        StartCoroutine(Pause());
    }

    void Update()
    {
        if (targetPoints.Count > 0 && !pause)
        {
            Transform targetPoint = targetPoints[currentTargetIndex];

            // Move the object towards the current target point
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);

            // Check if the object has reached the current target point
            if (transform.position == targetPoint.position)
            {
                // Move to the next target in the list, looping back to the start if necessary
                currentTargetIndex = (currentTargetIndex + 1) % targetPoints.Count;
            }
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(pauseTimer);
        pause = false;
    }
}