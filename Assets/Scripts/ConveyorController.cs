using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorController : MonoBehaviour
{
    public Transform target;
    public float speed;
    public ConveyorMode mode = ConveyorMode.Force;

    private void OnCollisionStay(Collision collision)
    {
        if (mode == ConveyorMode.Force)
        {
            if (collision.gameObject.CompareTag("Gem") ||
                collision.gameObject.CompareTag("Chest"))
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition((target.position - collision.transform.position) * speed, collision.GetContact(0).point, ForceMode.Force);
            }
        } else if (mode == ConveyorMode.Velocity)
        {
            //collision.gameObject.transform.position = Vector3.MoveTowards(collision.gameObject.transform.position, target.position, speed * Time.deltaTime);
            Rigidbody rigidbody1 = collision.gameObject.GetComponent<Rigidbody>();
            rigidbody1.velocity = (target.position - collision.transform.position) * speed;
        }

        
    }
}


public enum ConveyorMode
{
    Force, Velocity
}