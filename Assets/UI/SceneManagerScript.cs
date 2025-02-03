using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] public float creditsSpeed = 5f;
    [SerializeField] public GameObject creditsToMove;
    private Vector3 startPosition;
    public bool canMove;
    void Start()
    {
        startPosition = creditsToMove.transform.localPosition;
        canMove = false;
    }
    void Update()
    {
        if (canMove) {
            creditsToMove.transform.localPosition += creditsToMove.transform.up * creditsSpeed * Time.deltaTime;
        }
    }
    public void StartMovement()
    {
        if (!canMove)
        {
            canMove = true;
        }
        else {
            ResetMovement();
        }
    }
    public void ResetMovement()
    {
        canMove = false;
        creditsToMove.transform.localPosition = startPosition;
    }
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame(){
        Application.Quit();
        Debug.LogAssertion("QUIT");
    }
}
