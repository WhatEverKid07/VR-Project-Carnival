using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreKeeper : MonoBehaviour
{
    public Text highScoreText;
    public Text scoreText;

    private static int highScore;

    private void Start()
    {
        /*
        int score = Distance.distanceScoreStatic * PlayerController.coinScoreStatic;
        scoreText.text = score.ToString();

        if(score > highScore)
        {
            highScore = score;
        }

        highScoreText.text = highScore.ToString();








        coinScoreStatic = coinScore;
    
        if (collision.tag == "Coin")
        {
            coinCollection.Play();
            coinScore += 5;
            coinScoreText.text = coinScore.ToString();
            Debug.Log(coinScore);
            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "VendingMachine")
        {
            vendingMachineExplosion.Play();
            coinScore += 15;
            coinScoreText.text = coinScore.ToString();
            Debug.Log(coinScore);
        }
        */
    }
}
