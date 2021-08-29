using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenTooFar : MonoBehaviour
{
    public Transform origin;
    public float maxDistance;

    void Update()
    {
        if ((origin == null && (transform.position.magnitude > maxDistance))
            || (origin != null && Vector3.Distance(origin.position, transform.position) > maxDistance))
        {
            Destroy(gameObject);
        }    
    }
}
