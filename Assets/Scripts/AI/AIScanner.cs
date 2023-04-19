using Assets.Scripts.Core;
using Assets.Scripts.Needs.Scripts.ScriptableData;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIScanner : EnhancedBehaviour
    {
        #region Properties & Fields

        private NavMeshAgent m_NavMeshAgent;

        #endregion

        #region Public Methods

        /// <summary>
        /// Finds the closest transform that has <see cref="Component"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Nearest <see cref="Transform"/> if found or <see cref="null"/></returns>
        public Transform FindClosestNeedSatisfyer<T>(NeedSatisfyer[] needSatisfyers) where T : Component
        {
            Type type = typeof(T);
            UnityEngine.Object[] objects = FindObjectsOfType(type);

            Transform current = null;
            float currentDistance = 0.0f;

            int length = objects.Length;

            for (int i = 0; i < length; i++)
            {
                Transform candidate = objects[i].GameObject().transform;

                if (!IsReachable(candidate.position)) continue;

                if (!HasNeedSatisfyer(candidate, needSatisfyers)) continue;

                if (current == null)
                {
                    current = candidate;
                    currentDistance = Vector3.Distance(current.position, transform.position);
                    continue;
                }

                float foundDistance = Vector3.Distance(candidate.position, transform.position);
                bool isNewCloser = foundDistance < currentDistance;

                if (!isNewCloser) continue;
                current = candidate;
                currentDistance = foundDistance;
            }

            return current;
        }

        /// <summary>
        /// Checks if given position is reachable for <see cref="NavMeshAgent"/>
        /// </summary>
        /// <param name="position">Position to reach</param>
        /// <returns>true if it's reachable</returns>
        private bool IsReachable(Vector3 position)
        {
            NavMeshPath navMeshPath = new();

            bool pathExists = GetNavMeshAgent().CalculatePath(position, navMeshPath);
            bool isPathComplete = navMeshPath.status == NavMeshPathStatus.PathComplete;

            return pathExists && isPathComplete;
        }

        /// <summary>
        /// Checks if found transform's component has proper <see cref="NeedSatisfyer"/>
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="needSatisfyers">list of acceptable satisfyers</param>
        /// <returns></returns>
        private bool HasNeedSatisfyer(Transform transform, NeedSatisfyer[] needSatisfyers)
        {
            if (!transform.TryGetComponent(out NeedSatisfactionObject nso)) return false;

            for (int j = 0; j < needSatisfyers.Length; j++)
            {
                if (!nso.IsSameSatysfier(needSatisfyers[j]))
                    continue;
                return true;
            }
            return false;
        }
        #endregion

        #region Private Methods
        private NavMeshAgent GetNavMeshAgent() => m_NavMeshAgent == null ? m_NavMeshAgent = GetComponent<NavMeshAgent>() : m_NavMeshAgent;
        #endregion

    }
}