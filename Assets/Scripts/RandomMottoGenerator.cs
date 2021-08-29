using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomMottoGenerator : MonoBehaviour
{
    public string[] mottos;


    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = mottos[(int)UnityEngine.Random.Range(0, mottos.Length)];
    }

}
