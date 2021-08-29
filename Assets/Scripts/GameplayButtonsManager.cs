using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayButtonsManager : MonoBehaviour
{

    public GameObject instructionPanel;

    private void Start()
    {

        if (instructionPanel != null)
        {
            StopGame();
        } else
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void StopGame()
    {
        instructionPanel.SetActive(true);

        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Work()
    {
        Destroy(instructionPanel);
        StartGame();
    }

    public void Exit()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
