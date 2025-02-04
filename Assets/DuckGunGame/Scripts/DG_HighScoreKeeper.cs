using UnityEngine;
using TMPro;
using UnityEngine.UI; // Import UI for button functionality

public class DGHighScoreKeeper : MonoBehaviour
{
    public TMP_Text highScoreText;
    public TMP_Text finaleScoreText;
    public Button resetButton; // Assign this in the Inspector

    private int DG_HighScore;

    private void Start()
    {
        // Load the high score from PlayerPrefs
        DG_HighScore = PlayerPrefs.GetInt("DG_HighScore", 0);
        highScoreText.text = DG_HighScore.ToString();

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
        if (finaleScore > DG_HighScore)
        {
            DG_HighScore = finaleScore;
            PlayerPrefs.SetInt("DG_HighScore", DG_HighScore);
            PlayerPrefs.Save(); // Save to disk

            // Update the high score text
            highScoreText.SetText(DG_HighScore.ToString());
        }
    }

    public void ResetHighScore()
    {
        DG_HighScore = 0;
        PlayerPrefs.SetInt("DG_HighScore", 0);
        PlayerPrefs.Save();

        // Update UI
        highScoreText.SetText(0.ToString());
    }
}
