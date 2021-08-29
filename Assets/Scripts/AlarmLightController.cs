using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLightController : MonoBehaviour
{
    public Vector3 speed = new Vector3(0F, 2F, 0F);
    private void Update()
    {
        transform.Rotate(speed, Space.Self);
    }
}
