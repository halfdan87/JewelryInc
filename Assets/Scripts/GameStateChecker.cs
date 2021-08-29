using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateChecker : MonoBehaviour
{
    public TextMeshProUGUI counter;

    private HashSet<GameObject> itemsLeft = new HashSet<GameObject>();

    public LayerMask[] layerMasks;

    private Coroutine coroutine;

    private void Start()
    {
        NestController[] nestControllers = FindObjectsOfType<NestController>();
        
        foreach(NestController ctrl in nestControllers)
        {
            itemsLeft.Add(ctrl.gameObject);
        }

        counter.enabled = false;
    }

    private void FixedUpdate()
    {
        int sum = 0;

        foreach(LayerMask mask in layerMasks)
        {
            Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale, Quaternion.identity, mask);

            foreach (Collider col in hitColliders)
            {
                if (col.GetComponent<NestController>() != null || col.GetComponent<JewelryController>())
                {
                    sum++;
                }
            }
        }

        if ((sum == 0) && !counter.enabled)
        {
            coroutine = StartCoroutine(Timeout());
        }
        if ((sum > 0) && counter.enabled)
        {
            StopCoroutine(coroutine);
            counter.enabled = false;
        }
    }

    IEnumerator Timeout()
    {
        counter.enabled = true;
        for (int i = 5; i > 0; i--)
        {
            counter.fontSize = 64;
            counter.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        counter.enabled = false;
        GameManager.instance.events.InvokeLevelCleared();
    }

}
