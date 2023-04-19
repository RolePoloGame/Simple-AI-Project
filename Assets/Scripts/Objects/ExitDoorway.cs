using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Objects
{
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
        /// <summary>
        /// Calls <see cref="AISpawner"/>'s spawn method
        /// </summary>
        private void CallSpawnAgent()
        {
            AISpawner.Instance.SpawnAgent();
        }
        /// <summary>
        /// Gets parent of a given collider, disables it and destroys it
        /// </summary>
        /// <param name="other"></param>
        private void DestroyAgent(Collider other)
        {
            GameObject gameObject = other.transform.parent.gameObject;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        #endregion
    }
}