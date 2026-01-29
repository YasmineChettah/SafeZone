using UnityEngine;

public class XRSpawn : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
    }
}
