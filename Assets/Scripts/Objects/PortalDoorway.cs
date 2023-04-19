using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PortalDoorway : EnhancedBehaviour
{
    #region Properties & Fields
    [SerializeField]
    private PortalDoorway m_Exit;

    [SerializeField]
    private bool m_IsDelayed = false;
    [SerializeField]
    private float m_DelayTime = 5.5f;
    [SerializeField]
    private float m_Timer = 0.0f;
    #endregion

    #region Unity Methods
    void Update()
    {
        if (!m_IsDelayed) return;
        Log($"{name} is delayed... {m_Timer} < {m_DelayTime}");
        m_Timer -= Time.deltaTime;
        m_IsDelayed = m_Timer >= float.Epsilon;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Teleportable")) return;

        if (m_IsDelayed) return;
        Delay();
        m_Exit.Delay();

        Vector3 newPosition = m_Exit.transform.position;

        Transform parent = other.transform.parent; 
        bool hasHavMesh = parent.TryGetComponent(out NavMeshAgent navMesh);
        if (!hasHavMesh) return;
        navMesh.WarpWithPathPreservation(newPosition);
    }

    #endregion

    #region Private Methods

    private void Delay()
    {
        m_IsDelayed = true;
        m_Timer = m_DelayTime;
    }

    private IEnumerator ResetNavMesh(Vector3 newPosition, NavMeshAgent navMesh)
    {
        Vector3 destination = navMesh.destination;
        navMesh.ResetPath();
        navMesh.SetDestination(destination);
        DateTime dateTime = DateTime.Now;
        while (navMesh.pathPending || navMesh.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            if (dateTime - DateTime.Now > TimeSpan.FromMilliseconds(30000)) break;
            yield return null;
        }
        Log("Finished!");
        navMesh.isStopped = false;

        navMesh.transform.position = newPosition;
    }
    #endregion
}
