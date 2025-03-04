using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public SceneManagerScript sceneManagerScript;
    public List<MeshRenderer> meshRenderers;

    // Checks if the current collider entering is the Button and sets off OnPressed event.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onPressed?.Invoke();
            Debug.Log("I have been pressed");
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = false;
            }
        }

        if (other.tag == "StartButton" && !_deadTimeActive)
        {
            //onPressed?.Invoke();
            Debug.Log("I have been pressed");
            StartCoroutine(StartGameDelay());
            sceneManagerScript.ResetMovement();
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = false;
            }
        }

        if (other.tag == "OptionsButton" && !_deadTimeActive)
        {
            //onPressed?.Invoke();
            Debug.Log("I have been pressed");
            StartCoroutine(OptionsDelay());
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = false;
            }
        }

        if (other.tag == "QuitButton" && !_deadTimeActive)
        {
            onPressed?.Invoke();
            Debug.Log("I have been pressed");
            Application.Quit();

        }

        if (other.tag == "DuckDuckBombButton" && !_deadTimeActive)
        {
            //onPressed?.Invoke();
            Debug.Log("I have been pressed");
            StartCoroutine(DuckDuckBombDelay());
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = false;
            }
        }

        if (other.tag == "DuckFishButton" && !_deadTimeActive)
        {
            //onPressed?.Invoke();
            Debug.Log("I have been pressed");
            StartCoroutine(DuckFishDelay());
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = false;
            }
        }

        if (other.tag == "BackButton" && !_deadTimeActive)
        {
            //onPressed?.Invoke();
            Debug.Log("I have been pressed");
            StartCoroutine(BackDelay());
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = false;
            }
        }
    }

    // Coroutine that waits (x) seconds before executing
    private IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(1.5f);  // Wait for (x) seconds
        //SceneManager.LoadScene("SelectScene");  // Load the scene after the delay
        onPressed?.Invoke();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = true;
        }
    }

    private IEnumerator OptionsDelay()
    {
        yield return new WaitForSeconds(1.5f);  // Wait for (x) seconds
        onPressed?.Invoke();  // Load the scene after the delay
    }

    private IEnumerator DuckDuckBombDelay()
    {
        yield return new WaitForSeconds(1.5f);  // Wait for (x) seconds
        //SceneManager.LoadScene("DuckGunGame");  // Load the scene after the delay
        onPressed?.Invoke();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = true;
        }
    }

    private IEnumerator DuckFishDelay()
    {
        yield return new WaitForSeconds(1.5f);  // Wait for (x) seconds
        //SceneManager.LoadScene("DuckFishing");  // Load the scene after the delay
        onPressed?.Invoke();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = true;
        }
    }

    private IEnumerator BackDelay()
    {
        yield return new WaitForSeconds(1.5f);  // Wait for (x) seconds
        //SceneManager.LoadScene("MenuScene");  // Load the scene after the delay
        onPressed?.Invoke();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = true;
        }
    }

    // Checks if the current collider exiting is the Button and sets off OnReleased event. 
    // It will also call a Coroutine to make the button inactive for however long deadTime is set to.
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onReleased?.Invoke();
            Debug.Log("I have been released");
            //StartCoroutine(WaitForDeadTime());
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = true;
            }
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