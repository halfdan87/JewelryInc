using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestController : MonoBehaviour
{
    public GemType type;

    private GemController caught = null;

    public Transform holograph;

    public float fitSpead = 3f;

    public float elevation= 0.5f;

    public Vector3 targetRotation;


    private void OnTriggerEnter(Collider other)
    {
        if (caught == null && other.CompareTag("Gem"))
        {
            GemController gemController = other.GetComponent<GemController>();

            if (!gemController.IsNested && gemController.Fits(type))
            {
                gemController.IsNested = true;

                GameManager.instance.events.InvokeGemFitted(this, gemController);

                Rigidbody rg = gemController.GetComponent<Rigidbody>();

                rg.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                rg.isKinematic = true;

                caught = gemController;

                gemController.transform.parent = transform;

                Destroy(holograph.gameObject.GetComponent<MeshRenderer>());

                StartCoroutine(FitGem());
            }
        }
    }
    IEnumerator FitGem()
    {
        float originalZ = transform.position.z;
        caught.transform.localPosition = caught.transform.localPosition - new Vector3(0, 0, 3F);

        while ((Quaternion.Angle(caught.transform.rotation, holograph.rotation) > 5F)
            && (Vector3.Distance(caught.transform.position, transform.position) > 0.1F))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, originalZ + elevation), Time.deltaTime * fitSpead / 2);

            caught.transform.position = Vector3.MoveTowards(caught.transform.position, holograph.position, Time.deltaTime *  (fitSpead + 0.3F));
            caught.transform.rotation = Quaternion.Lerp(caught.transform.rotation, holograph.rotation, Time.deltaTime * (fitSpead + 0.3F));

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), 2F);

            yield return null;
        }

        GameObject created = GameManager.instance.objectGenerator.GenerateJewelryByType(caught.type, caught.transform.position);
        created.transform.position = transform.position;
        created.transform.rotation = transform.rotation;

        Destroy(caught.gameObject);
        Destroy(gameObject);
    }
}
