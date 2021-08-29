using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public Vector3 gravity = Physics.gravity;
    void Awake()
    {
        Physics.gravity = gravity;

        Destroy(gameObject);
    }
}
