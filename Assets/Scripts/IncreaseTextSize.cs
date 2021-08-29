using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncreaseTextSize : MonoBehaviour
{
    private TextMeshProUGUI label;

    public float speed = 1;

    void Start()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        label.fontSize += speed;
    }
}
