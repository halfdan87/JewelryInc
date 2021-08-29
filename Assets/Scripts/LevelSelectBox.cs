using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelectBox : MonoBehaviour
{
    public int level;
    public Button button;

    public TextMeshProUGUI levelNumgerLabel;
    public TextMeshProUGUI scoreLabel;

    private void Start()
    {
        int score = GetScore(level);
        int previous = GetScore(level - 1);

        levelNumgerLabel.SetText(level.ToString());
        scoreLabel.SetText(score.ToString());

        button.interactable = ((level == 1) || (score == 0 && previous > 0));
    }

    private int GetScore(int level)
    {
        return PlayerPrefs.GetInt("level" + level);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Click()
    {
        SceneManager.LoadScene("Level" + level.ToString());
    }

}
