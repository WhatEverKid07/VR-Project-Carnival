using UnityEngine;
using TMPro;
using UnityEngine.UI; // Import UI for button functionality

public class DFHighScoreKeeper : MonoBehaviour
{
    public TMP_Text highScoreText;
    public TMP_Text finaleScoreText;
    public Button resetButton; // Assign this in the Inspector

    private int DF_HighScore;

    private void Start()
    {
        // Load the high score from PlayerPrefs
        DF_HighScore = PlayerPrefs.GetInt("DF_HighScore", 0);
        highScoreText.text = DF_HighScore.ToString();

        // Assign button event listener
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetHighScore);
        }
    }

    public void SetFinalScore(int finaleScore)
    {
        finaleScoreText.SetText(finaleScore.ToString());

        // Check if new high score is achieved
        if (finaleScore > DF_HighScore)
        {
            DF_HighScore = finaleScore;
            PlayerPrefs.SetInt("DF_HighScore", DF_HighScore);
            PlayerPrefs.Save(); // Save to disk

            // Update the high score text
            highScoreText.SetText(DF_HighScore.ToString());
        }
    }

    public void ResetHighScore()
    {
        DF_HighScore = 0;
        PlayerPrefs.SetInt("DF_HighScore", 0);
        PlayerPrefs.Save();

        highScoreText.SetText(DF_HighScore.ToString());
    }
}
