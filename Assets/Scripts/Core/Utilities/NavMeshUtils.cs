using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Core.Utilities
{
    public static class NavMeshUtils
    {
        /// <summary>
        /// Warps NavMeshAgent to a given position if possible and recalculates path to an old target
        /// </summary>
        public static void WarpWithPathPreservation(this NavMeshAgent navMesh, Vector3 warpTarget)
        {
            NavMeshPath path = new();
            if (!NavMesh.CalculatePath(warpTarget, navMesh.destination, NavMesh.AllAreas, path)) return; //no path found

            navMesh.isStopped = true;
            navMesh.Warp(warpTarget);
            navMesh.path = path;
            navMesh.isStopped = false;
        }
    }
}