using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    internal int score = 0;
    internal int cleaningCost = 0;

    [Serializable]
    public struct GemTypeToScore
    {
        public GemType type;
        public int score;
    }
    public GemTypeToScore[] fitScores;
    public GemTypeToScore[] boxScores;

    public TextMeshProUGUI scoreLabel;

    void Start()
    {
        GameManager.instance.events.GemFitted += GemFitted;
        GameManager.instance.events.JewelryBoxed += JewelryBoxed;
        GameManager.instance.events.GemCreated += GemCreated;

        UpdateLabel();
    }

    public void GemFitted(object sender, GemArgs gem)
    {
        score += GetScore(fitScores, gem.Gem.type);
        cleaningCost += 100;

        UpdateLabel();
    }

    public void JewelryBoxed(object sender, JewelryArgs jewelry)
    {
        score += GetScore(boxScores, jewelry.Jewelry.type);
        cleaningCost -= 100;

        UpdateLabel();
    }
    public void GemCreated()
    {
        cleaningCost += 1;
    }

    private void UpdateLabel()
    {
        scoreLabel.text = score.ToString();
    }

    private static int GetScore(GemTypeToScore[] scores, GemType type)
    {
        foreach (GemTypeToScore mapping in scores)
        {
            if (mapping.type == type)
            {
                return mapping.score;
            }
        }
        return 0;
    }
}
