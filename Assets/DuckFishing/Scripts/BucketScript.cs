using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BucketScript : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private int pointsIncrease;
    public int currentPoints; // [HideInInspector]

    private DuckPointed ducMovingScript;

    private void Start()
    {
        currentPoints = 0;
        UpdatePointsText();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Duck"))
        {
            ducMovingScript = collision.gameObject.GetComponent<DuckPointed>();
            if (ducMovingScript.pointed == false)
            {
                currentPoints += pointsIncrease;
                ducMovingScript.pointed = true;
                UpdatePointsText();
            }
        }
    }
    private void UpdatePointsText()
    {
        pointsText.text = currentPoints.ToString();
    }
}
