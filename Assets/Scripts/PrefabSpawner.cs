using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabs;

    public float pause;

    public float decrement;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(pause);

        GameObject prefab = prefabs[(int)UnityEngine.Random.Range(0, prefabs.Length)];

        GameObject gem = Instantiate(prefab, transform.position, prefab.transform.rotation);

        StartCoroutine(Spawn());

        if (pause > 0)
        {
            pause -= decrement;
        }

        pause = Mathf.Clamp(pause, 0.1F, 100F);
    }
}
