using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jewelry"))
        {
            GameManager.instance.events.InvokeGemBoxed(this, other.GetComponent<JewelryController>());
        }
    }
}
