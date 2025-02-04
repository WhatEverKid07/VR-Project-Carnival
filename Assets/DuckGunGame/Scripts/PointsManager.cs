using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private int duckPoints;
    [SerializeField] private int bombPoints;

    //[SerializeField] private HighScoreKeeper highScoreScript;

    public TMP_Text pointsText;

    [HideInInspector] public int currentPoints;

    private void Start()
    {
        currentPoints = 0;
        UpdatePointsText();
    }

    public void AddPoints()
    {
        currentPoints += duckPoints;
        UpdatePointsText();
    }

    public void RemovePoints()
    {
        currentPoints -= bombPoints;
        UpdatePointsText();
    }

    private void UpdatePointsText()
    {
        pointsText.text = currentPoints.ToString();
    }
}
