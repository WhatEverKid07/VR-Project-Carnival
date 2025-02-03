using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/*
public class RecenterOrigin : MonoBehaviour
{
    public Transform head;
    public Transform origin;
    public Transform target;

    public InputActionProperty recenterButton;

    public void Recenter()
    {
        Vector3 offset = head.position - origin.position;
        offset.y = 0;
        origin.position = target.position - offset;

        Vector3 targetForward = target.forward;
        targetForward.y = 0;
        Vector3 cameraForward = head.forward;
        cameraForward.y = 0;

        float angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);

        origin.RotateAround(head.position, Vector3.up, angle);
    }

    // Update is called once per frame
    void Update()
    {
        if (recenterButton.action.WasPressedThisFrame())
        {
            Recenter();
        }
    }
}
*/

public class RecenterOrigin : MonoBehaviour
{
    public Transform head;
    public Transform origin;
    public Transform target;

    public InputActionProperty recenterButton;

    public void Recenter()
    {
        if (head == null || origin == null || target == null)
        {
            Debug.LogError("One or more references are missing in the inspector.");
            return;
        }

        Debug.Log("Recenter triggered");

        // Calculate the offset
        Vector3 offset = head.position - origin.position;
        offset.y = 0;  // Ignore the vertical offset

        // Move the origin to target position minus the offset
        origin.position = target.position - offset;

        // Calculate the angle difference between the head and target directions
        Vector3 targetForward = target.forward;
        targetForward.y = 0;
        Vector3 cameraForward = head.forward;
        cameraForward.y = 0;

        float angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);

        // Debug the calculated angle
        Debug.Log("Calculated angle: " + angle);

        // Apply the rotation to the origin
        origin.rotation = Quaternion.Euler(0, angle, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (recenterButton.action.WasPressedThisFrame())
        {
            Recenter();
        }
    }
}
