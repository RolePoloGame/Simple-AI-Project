using Assets.Scripts.Core;
using Assets.Scripts.Core.Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Objects
{
    public class PortalDoorway : EnhancedBehaviour
    {
        #region Properties & Fields
        [SerializeField]
        private PortalDoorway m_Exit;

        [SerializeField]
        private bool m_IsDelayed = false;
        [SerializeField]
        private float m_DelayTime = 2.5f; //seconds
        [SerializeField]
        private float m_Timer = 0.0f;
        #endregion

        #region Unity Methods
        private void Update() => HandleDelay();

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Teleportable")) return;

            if (m_IsDelayed) return;
            //Delays the instant teleport on an exit so object doesn't enter infinite loop of teleportation
            m_Exit.EnableDelay();

            Vector3 newPosition = m_Exit.transform.position;

            Transform parent = other.transform.parent;
            bool hasHavMesh = parent.TryGetComponent(out NavMeshAgent navMesh);

            if (!hasHavMesh) return;
            navMesh.WarpWithPathPreservation(newPosition);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Enables delay for a given <see cref="m_DelayTime"/> given in seconds
        /// </summary>
        private void EnableDelay()
        {
            m_IsDelayed = true;
            m_Timer = m_DelayTime;
        }

        /// <summary>
        /// Handles delay when enabled by <see cref="EnableDelay"/>
        /// </summary>
        private void HandleDelay()
        {
            if (!m_IsDelayed) return;

            Log($"{name} is delayed... {m_Timer}");
            m_Timer -= Time.deltaTime;
            m_IsDelayed = m_Timer >= float.Epsilon;
        }

        #endregion
    }
}