using System.Collections.Generic;
using UnityEngine;

public class GemCatcher : MonoBehaviour
{
    public float maxGrabbed = 1;

    private bool grabbing;

    private List<GemController> caught = new List<GemController>();
    private List<JewelryController> caughtRings = new List<JewelryController>();

    public float throwForce = 1F;

    private void Start()
    {
        GameManager.instance.events.GrabbingChanged += OnGrabbingChanged;
    }

    public void OnGrabbingChanged(object sender, BoolArgs args)
    {
        grabbing = args.State;

        if (!grabbing)
        {
            foreach(GemController obj in caught)
            {
                GameObject created = GameManager.instance.objectGenerator.GenerateGemByType(obj.type, obj.transform.position);

                created.transform.rotation = obj.transform.rotation;

                Rigidbody rigidbody1 = created.GetComponent<Rigidbody>();

                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                rigidbody1.AddForce(new Vector3(mouseX, mouseY, 0f) * throwForce, ForceMode.Impulse);

                Destroy(obj.gameObject);
            }
            caught.Clear();

            foreach (JewelryController obj in caughtRings)
            {
                GameObject created = GameManager.instance.objectGenerator.GenerateJewelryByType(obj.type, obj.transform.position);

                created.transform.rotation = obj.transform.rotation;

                Rigidbody rigidbody1 = created.GetComponent<Rigidbody>();

                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                rigidbody1.AddForce(new Vector3(mouseX, mouseY, 0f) * throwForce, ForceMode.Impulse);

                Destroy(obj.gameObject);
            }
            caughtRings.Clear();
            
        }
    }

    private void OnTriggerStay (Collider other)
    {
        if (!grabbing || (caught.Count + caughtRings.Count) == maxGrabbed)
        {
            return;
        }
        if (other.CompareTag("Gem"))
        {
            GemController gemController = other.gameObject.GetComponent<GemController>();
            if (gemController.IsNested)
            {
                return;
            }

            Destroy(other.gameObject.GetComponent<Rigidbody>());
            other.gameObject.transform.parent = transform;
            Destroy(other.gameObject.GetComponent<MeshCollider>());
            other.gameObject.tag = "Untagged";
            caught.Add(gemController);
            other.transform.position = transform.position;
        }
        else if (other.CompareTag("Jewelry"))
        {
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            other.gameObject.transform.parent = transform;

            foreach(MeshCollider col in other.gameObject.GetComponentsInChildren<MeshCollider>())
            {
                Destroy(col);
            }

            other.gameObject.tag = "Untagged";
            caughtRings.Add(other.gameObject.GetComponent<JewelryController>());
            other.transform.position = transform.position;
        }
    }

}
