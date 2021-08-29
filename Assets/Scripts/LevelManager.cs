using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int levelNumber;

    public String nextLevel;

    public int satisfactoryThreshold;



    public ScoreManager scoreManager;

    public GameObject summaryPanel;
    public TextMeshProUGUI pieceWorkValueLabel;
    public TextMeshProUGUI cleanUpCostsLabel;
    public TextMeshProUGUI payCheckLabel;
    public TextMeshProUGUI assessmentLabel;

    public Button nextLevelButton;

    void Start()
    {
        GameManager.instance.events.TimeUp += EndLevel;
        GameManager.instance.events.LevelCleared += EndLevel;
        summaryPanel.SetActive(false);
    }

    public void EndLevel()
    {
        summaryPanel.SetActive(true);

        pieceWorkValueLabel.text = scoreManager.score.ToString();
        cleanUpCostsLabel.text = scoreManager.cleaningCost.ToString();

        int totalScore = scoreManager.score - scoreManager.cleaningCost;
        payCheckLabel.text = (totalScore).ToString();

        bool satisfactory = (totalScore >= satisfactoryThreshold);

        if (satisfactory)
        {
            SaveScores(totalScore);
        }

        nextLevelButton.interactable = satisfactory;
        assessmentLabel.text = satisfactory ? "satisfactory" : "disappointing";

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    private void SaveScores(int score)
    {
        PlayerPrefs.SetInt("level" + levelNumber.ToString(), scoreManager.score);
        PlayerPrefs.Save();
    }
}
