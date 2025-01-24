using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class VRButton : MonoBehaviour
{
    // Time that the button is set inactive after release
    public float deadTime = 1.0f;
    // Bool used to lock down button during its set dead time
    private bool _deadTimeActive = false;

    // Public Unity Events we can use in the editor and tie other functions to.
    public UnityEvent onPressed, onReleased;

    // Checks if the current collider entering is the Button and sets off OnPressed event.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onPressed?.Invoke();
            Debug.Log("I have been pressed");
        }

        if (other.tag == "StartButton" && !_deadTimeActive)
        {
            onPressed?.Invoke();
            Debug.Log("I have been pressed");
            StartCoroutine(LoadSceneAfterDelay());  // Start the coroutine here
        }
    }

    // Coroutine that waits for 1.5 seconds before loading the scene
    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);  // Wait for 1.5 seconds
        SceneManager.LoadScene("SampleScene");  // Load the scene after the delay
    }

    // Checks if the current collider exiting is the Button and sets off OnReleased event. 
    // It will also call a Coroutine to make the button inactive for however long deadTime is set to.
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onReleased?.Invoke();
            Debug.Log("I have been released");
            StartCoroutine(WaitForDeadTime());
        }
    }

    // Locks button activity until deadTime has passed and reactivates button activity.
    private IEnumerator WaitForDeadTime()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }
}