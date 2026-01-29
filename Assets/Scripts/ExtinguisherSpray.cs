using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class ExtinguisherSpray : MonoBehaviour
{
    public Transform sprayRoot;     // le parent du VFX (Spray)
    public Collider sprayTrigger;   // SprayTrigger
    public float extinguishRate = 2f;

    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    ParticleSystem[] systems;
    bool isHeld;
    bool isSpraying;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (sprayRoot != null)
            systems = sprayRoot.GetComponentsInChildren<ParticleSystem>(true);

        SetSpray(false);

        grab.selectEntered.AddListener(_ => isHeld = true);
        grab.selectExited.AddListener(_ => { isHeld = false; SetSpray(false); });

        grab.activated.AddListener(_ => { if (isHeld) SetSpray(true); });
        grab.deactivated.AddListener(_ => SetSpray(false));
    }

    void SetSpray(bool on)
    {
        isSpraying = on;

        if (sprayTrigger) sprayTrigger.enabled = on;

        if (systems != null)
        {
            foreach (var ps in systems)
            {
                if (on) ps.Play(true);
                else ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!isSpraying) return;

        var fire = other.GetComponentInParent<Fire>();
        if (fire != null)
            fire.Extinguish(extinguishRate * Time.deltaTime);
    }
}
