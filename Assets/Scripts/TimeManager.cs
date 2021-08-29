using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    public float gameTime;

    public TextMeshProUGUI timeLabel;

    private float currentTime = 0f;

    void Update()
    {
        if (currentTime >= gameTime)
        {
            GameManager.instance.events.InvokeTimeUp();
        } else
        {
            currentTime += Time.deltaTime;
        }

        UpdateLabel();
    }

    private void UpdateLabel()
    {
        timeLabel.text = FormatCurrentTime();
    }

    private string FormatCurrentTime()
    {
        float hours = (currentTime / gameTime) * 8F;

        int fullHours = (int)hours;

        float minutes = (hours - fullHours) * 60;

        return string.Format("{0:00}:{1:00}", fullHours + 6, (int)minutes); 
    }
}
