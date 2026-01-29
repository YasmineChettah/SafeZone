using UnityEngine;


public class FindableItem : MonoBehaviour
{
    public string itemId = "item";
    bool found = false;

    void Awake()
    {
        var grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab != null)
            grab.selectEntered.AddListener(_ => MarkFound());
    }

    public void MarkFound()
    {
        if (found) return;
        found = true;

        FindGameManager.Instance?.OnItemFound(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!found && other.CompareTag("Player"))
            MarkFound();
    }
}
