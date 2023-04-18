using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIScanner : EnhancedBehaviour
{
    #region Properties & Fields

    private NavMeshAgent m_NavMeshAgent;
    //TODO: Implement Range
    private bool m_HasRange = false;
    private float m_Range = 0.0f;
    #endregion

    #region Public Methods

    /// <summary>
    /// Finds the closest transform that has <see cref="Component"/> within given <see cref="m_Range"/> if specified. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>Nearest <see cref="Transform"/> if found or <see cref="null"/></returns>
    public Transform FindClosestComponent<T>() where T : Component
    {
        Type type = typeof(T);
        GameObject[] objects = (GameObject[])FindObjectsOfType(type);

        Transform finalTransform = null;
        float finalTransformDistance = 0.0f;
        for (int i = 0; i < objects.Length; i++)
        {
            Transform foundTransform = objects[i].transform;
            //Check if found transforms are reachable
            NavMeshPath navMeshPath = new();
            bool pathExists = GetNavMeshAgent().CalculatePath(foundTransform.position, navMeshPath);
            bool isPathComplete = navMeshPath.status == NavMeshPathStatus.PathComplete;

            if (!pathExists || !isPathComplete) continue;

            // if it's the only or first found transform...
            if (finalTransform == null)
            {
                //...save it
                finalTransform = foundTransform;
                finalTransformDistance = Vector3.Distance(finalTransform.position, transform.position);
                continue;
            }
            //compare distance to find closest of the two
            float foundDistance = Vector3.Distance(foundTransform.position, transform.position);

            bool isNewCloser = foundDistance < finalTransformDistance;

            if (!isNewCloser)
                continue;
            //replace current with the new one
            finalTransform = foundTransform;
            finalTransformDistance = foundDistance;

        }

        return finalTransform;
    }
    #endregion

    #region Private Methods
    private NavMeshAgent GetNavMeshAgent() => m_NavMeshAgent == null ? m_NavMeshAgent = GetComponent<NavMeshAgent>() : m_NavMeshAgent;
    #endregion

}
