using System.Collections;
using UnityEngine;
using TMPro;

public class TimerScriptFishing : MonoBehaviour
{
    [SerializeField] private int minutes;
    [SerializeField] private int seconds;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject endCanvas;

    private float totalTimeInSeconds;
    private bool isRunning = false;

    [SerializeField] private DFHighScoreKeeper highScoreScript;
    [SerializeField] private BucketScript pointsManagerScript;

    private void Start()
    {
        SetTimer(minutes, seconds);
        StartTimer();
    }

    public void SetTimer(int minutes, int seconds)
    {
        totalTimeInSeconds = minutes * 60 + seconds;
        UpdateTimerText();
    }
    public void StartTimer()
    {
        if (!isRunning)
        {
            StartCoroutine(TimerCountdown());
        }
    }
    public void StopTimer()
    {
        isRunning = false;
        StopAllCoroutines();
    }

    private IEnumerator TimerCountdown()
    {
        isRunning = true;

        while (totalTimeInSeconds > 0)
        {
            yield return new WaitForSeconds(1);
            totalTimeInSeconds--;
            UpdateTimerText();
        }

        isRunning = false;
        OnTimerEnd();
    }
    private void UpdateTimerText()
    {
        int remainingMinutes = Mathf.FloorToInt(totalTimeInSeconds / 60);
        int remainingSeconds = Mathf.FloorToInt(totalTimeInSeconds % 60);
        timerText.text = string.Format("{0:D1}:{1:D1}", remainingMinutes, remainingSeconds);
    }
    private void OnTimerEnd()
    {
        Debug.Log("Timer has ended!");
        //end game and anything else
        endCanvas.SetActive(true);
        highScoreScript.SetFinalScore(pointsManagerScript.currentPoints);
    }
}
