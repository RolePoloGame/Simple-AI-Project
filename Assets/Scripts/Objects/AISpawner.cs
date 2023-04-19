using Assets.Scripts.Core;
using UnityEngine;

public class AISpawner : Singleton<AISpawner>
{
    #region Properties & Fields
    [SerializeField]
    private GameObject m_AgentPrefab;
    private ulong m_AgentCounter = 0;
    #endregion

    private void OnEnable()
    {
        SpawnAgent();
    }

    #region Public method
    /// <summary>
    /// Spawns a new instance of an AI Agent prefab
    /// </summary>
    public void SpawnAgent()
    {
        GameObject spawnedAgend = Instantiate(m_AgentPrefab, transform.position, Quaternion.identity, transform);
        spawnedAgend.name = $"Agent{m_AgentCounter}";
        spawnedAgend.GetComponent<AIController>().SetActive(true);
        m_AgentCounter++;
    }
    #endregion
}
