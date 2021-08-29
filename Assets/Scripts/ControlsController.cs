using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsController : MonoBehaviour
{
    public float gravity = 9.81F;

    private void Start()
    {
        Physics.gravity = new Vector3(0, 0, 0.5f) * gravity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }
}
