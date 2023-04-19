using UnityEngine;

public class ExitDoorway : EnhancedBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Teleportable")) return;

    }
}
