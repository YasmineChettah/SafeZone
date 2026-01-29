using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public GameFlowManager flow;
    public string playerTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        // Remonte au parent racine (XR Origin)
        Transform root = other.transform.root;

        Debug.Log($"Exit trigger touché par : {other.name} root={root.name} tag={root.tag}");

        if (root.CompareTag(playerTag))
        {
            flow.PlayerExited();
        }
    }
}
