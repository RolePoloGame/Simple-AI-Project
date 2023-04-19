using UnityEngine;
using UnityEngine.AI;

public static class NavMeshUtils
{
    public static void WarpWithPathPreservation(this NavMeshAgent navMesh, Vector3 warpTarget)
    {
        NavMeshPath path = new();
        if (!NavMesh.CalculatePath(warpTarget, navMesh.destination, NavMesh.AllAreas, path)) return;
        navMesh.isStopped = true;
        navMesh.Warp(warpTarget);
        navMesh.path = path;
        navMesh.isStopped = false;
    }
}
