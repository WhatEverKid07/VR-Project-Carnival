using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BucketScript : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private int pointsIncrease;
    private int currentPoints;

    private void Start()
    {
        currentPoints = 0;
        UpdatePointsText();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Duck"))
        {
            currentPoints += pointsIncrease;
            UpdatePointsText();
        }
    }
    private void UpdatePointsText()
    {
        pointsText.text = string.Format("{0}\n{1}", "Points", currentPoints);
    }
}
