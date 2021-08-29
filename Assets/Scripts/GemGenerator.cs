using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemGenerator : MonoBehaviour
{
    public float pause = 1;
    public float decrement = 0.01F;
    public GameObject[] prefabs;

    public Vector3 initialAngles;

    [Serializable]
    public struct GemTypeToPrefab
    {
        public GemType type;
        public GameObject prefab;
    }
    public GemTypeToPrefab[] rings;

    void Start()
    {
        StartCoroutine(GenerateGem());

        GameManager.instance.objectGenerator = this;
    }

    public GameObject GenerateGemByType(GemType type, Vector3 position)
    {
        foreach (GameObject prefab in prefabs)
        {
            if (prefab.GetComponent<GemController>().type == type)
            {
                return Instantiate(prefab, position, Quaternion.identity);
            }
        }
        throw new System.Exception("No such type: " + type);
    }

    public GameObject GenerateJewelryByType(GemType type, Vector3 position)
    {
        foreach (GemTypeToPrefab mapping in rings)
        {
            if (mapping.type == type)
            {
                return Instantiate(mapping.prefab, position, Quaternion.identity);
            }
        }
        throw new System.Exception("No such type: " + type);
    }

    IEnumerator GenerateGem()
    {
        yield return new WaitForSeconds(pause);

        GameObject prefab = prefabs[(int)UnityEngine.Random.Range(0, prefabs.Length)];

        GameObject gem = Instantiate(prefab, transform.position, Quaternion.identity);

        gem.GetComponent<DestroyWhenTooFar>().origin = transform;

        gem.transform.rotation = Quaternion.Euler(initialAngles);

        GameManager.instance.events.InvokeGemCreated();

        StartCoroutine(GenerateGem());

        if (pause > 0)
        {
            pause -= decrement;
        }

        pause = Mathf.Clamp(pause, 0.1F, 100F);
    }


}
