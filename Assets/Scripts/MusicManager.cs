using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public bool startOnLoad;

    void Start()
    {
        if (startOnLoad)
        {
            GetComponent<AudioSource>().Play();
        }
    }

}
