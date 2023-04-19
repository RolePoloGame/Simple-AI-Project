using Assets.Scripts.Core;
using UnityEngine;

public class ExitDoorway : Singleton<ExitDoorway>
{   
    #region Unity methods
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Teleportable")) return;
        DestroyAgent(other);
        CallSpawnAgent();
    } 
    #endregion

    #region Private methods
    private void CallSpawnAgent()
    {
        AISpawner.Instance.SpawnAgent();
    }

    private void DestroyAgent(Collider other)
    {
        GameObject gameObject = other.transform.parent.gameObject;
        gameObject.SetActive(false);
        Destroy(gameObject);
    } 
    #endregion
}
