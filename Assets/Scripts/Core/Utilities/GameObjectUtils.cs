using UnityEngine;

public static class GameObjectUtils
{
    /// <summary>
    /// Returns component of given type. If component is missing from <see cref="GameObject"/>, Adds one.
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        if (!gameObject.TryGetComponent(out T component))
            component = gameObject.AddComponent<T>();
        return component;
    }
    /// <summary>
    /// Returns component of given type which was stashed in an . If component is missing from <see cref="GameObject"/>, Adds one.
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static T GetStoredComponent<T>(this GameObject gameObject, ref T element) where T : Component
    {
        if (element == null)
            return element = gameObject.GetOrAddComponent<T>();
        return element;
    }

    public static Bounds GetMeshBounds(this GameObject go)
    {
        MeshFilter[] meshFilters = go.GetComponentsInChildren<MeshFilter>();

        if (meshFilters.Length == 0)
            return new Bounds();

        Bounds bound = meshFilters[0].mesh.bounds;
        for (int i = 1; i < meshFilters.Length; i++)
            bound.Encapsulate(meshFilters[i].mesh.bounds);
        return bound;
    }

    /// <summary>
    /// Checks if index is in bounds of given size
    /// </summary>
    /// <param name="index">checked index</param>
    /// <param name="exlusiveMax">exlusiveMax exclusive value </param>
    /// <param name="inclusiveMin">inclusiveMin inclusive value </param>
    public static bool EvaluateIndex(this int index, int exlusiveMax, int inclusiveMin = 0) => index >= inclusiveMin && index < exlusiveMax;
}
