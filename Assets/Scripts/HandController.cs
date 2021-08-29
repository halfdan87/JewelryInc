using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private float baseHeight, currentHeight;
    public float highHeight;
    public float grabbingReach = 0.5F;

    public float speed = 1F;
    public float maxAngle = 25F;

    private Vector3 currentAngles;

    private Quaternion baseRotation;

    private Animator myAnimator;

    private Rigidbody myRigidbody;

    private bool grabbing = false;

    public Transform minPosition, maxPosition;

    public Vector3 randomSeizure = Vector3.zero;

    private void Start()
    {
        GameManager.instance.events.GemCaught += GemCaught;
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        myAnimator = GetComponent<Animator>();

        myRigidbody = GetComponent<Rigidbody>();

        currentHeight = baseHeight = myRigidbody.position.z;

        baseRotation = myRigidbody.rotation;

        currentAngles = Vector3.zero;
    }

    private void GemCaught(object sender, GemArgs args)
    {
        currentHeight += (grabbing ? -grabbingReach : 0);
    }

    private void FixedUpdate()
    {
        SetAngle();

        SetPosition();

        SetGrabbing();
    }

    private void SetGrabbing()
    {
        bool grabbing = Input.GetMouseButton(0);

        if (grabbing != this.grabbing)
        {
            this.grabbing = grabbing;
            myAnimator.SetBool("Grabbing", grabbing);
            GameManager.instance.events.InvokeGrabbingChanged(this, grabbing);

            currentHeight += (grabbing ? grabbingReach : -grabbingReach);
        }

    }

    private void SetPosition()
    {
        float mouseX = Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) + UnityEngine.Random.Range(-randomSeizure.x, randomSeizure.x);
        float mouseY = Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1) + UnityEngine.Random.Range(-randomSeizure.y, randomSeizure.y);

        Vector3 newPos = myRigidbody.position + (new Vector3(mouseX, mouseY, 0) * speed);

        newPos = new Vector3(
            Mathf.Clamp(newPos.x, minPosition.position.x, maxPosition.position.x),
            Mathf.Clamp(newPos.y, minPosition.position.y, maxPosition.position.y), 
            currentHeight);

        myRigidbody.MovePosition(Vector3.Lerp(myRigidbody.position, newPos, 0.5F));
    }

    private void SetAngle()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotX = 0, rotY, rotZ;

        if (Mathf.Abs(mouseX) > 0.2F)
        {
            rotZ = -Mathf.Sign(mouseX) * Mathf.Lerp(0F, maxAngle, Mathf.Abs(mouseX));
        } else
        {
            rotZ = currentAngles.z;
        }

        if (!grabbing && Mathf.Abs(mouseY) > 0.2F)
        {
            rotY = Mathf.Sign(mouseY) * Mathf.Lerp(0F, maxAngle, Mathf.Abs(mouseY));
        } else
        {
            rotY = currentAngles.y;
        }

        currentAngles = new Vector3(rotX, rotY, rotZ);

        Quaternion target =  Quaternion.Lerp(myRigidbody.rotation, baseRotation * Quaternion.Euler(currentAngles), 0.5F);

        myRigidbody.MoveRotation(target);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Conveyor"))
        {
            currentHeight = baseHeight - highHeight + (grabbing ? grabbingReach : -grabbingReach);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Conveyor"))
        {
            currentHeight = baseHeight + (grabbing ? grabbingReach : -grabbingReach);
        }
    }

}
