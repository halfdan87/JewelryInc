using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;

    public Vector3 scale;
    public Vector3 offset;

    void Update()
    {
        Vector3 newPos = Vector3.Scale(target.position, scale) + offset;

        transform.position = Vector3.Lerp(transform.position, newPos, 0.5F);
    }
}
