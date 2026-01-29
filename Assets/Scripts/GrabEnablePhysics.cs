using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class GrabEnablePhysics : MonoBehaviour
{
    Rigidbody rb;
    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Départ: accroché
        rb.isKinematic = true;
        rb.useGravity = false;

        grab.selectEntered.AddListener(_ => OnGrab());
        grab.selectExited.AddListener(_ => OnRelease());
    }

    void OnGrab()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    void OnRelease()
    {
        if (isActiveAndEnabled)
            StartCoroutine(EnablePhysicsNextFrame());
    }

    IEnumerator EnablePhysicsNextFrame()
    {
        yield return null;

        rb.isKinematic = false;
        rb.useGravity = true;

        rb.WakeUp();
    }
}
