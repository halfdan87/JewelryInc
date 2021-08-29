using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    internal EventBus events;

    internal static GameManager instance;

    internal GemGenerator objectGenerator;

    void Awake()
    {
        events = new EventBus();

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }
}
