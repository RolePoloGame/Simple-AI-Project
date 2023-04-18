using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AIScanner))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIController : EnhancedBehaviour
{
    #region Properties & Fields
    [SerializeField]
    private AIState[] m_States;
    [SerializeField, ReadOnly]
    private AIState m_CurrentState = null;

    private AIScanner m_AIScanner;
    private NavMeshAgent m_NavMeshAgent;

    [SerializeField]
    private bool m_IsAwake = false;

    [SerializeField]
    private bool m_ChooseStateAtRandom = true;
    #endregion

    #region Unity Methods

    void Awake()
    {
        if (!m_ChooseStateAtRandom) return;
        int index = Random.Range(0, m_States.Length);
        m_CurrentState = m_States[index];
        Log($"Current State set! [{index}]");
        Log($"Current State is: {m_CurrentState}");
    }

    private void Update()
    {
        if (!m_IsAwake) return;

        if (!HasState()) return;

        HandleCurrentState();
    }

    #endregion

    #region Public Methods
    public AIScanner GetAIScanner() => m_AIScanner == null ? m_AIScanner = GetComponent<AIScanner>() : m_AIScanner;
    /// <summary>
    /// Sets target for a NavMeshAgent
    /// </summary>
    /// <param name="position">Position to which to go</param>
    public void SetGoToTarget(Vector3 position)
    {
        GetNavMeshAgent().isStopped = true;
        GetNavMeshAgent().ResetPath();
        GetNavMeshAgent().SetDestination(position);
    }

    public bool GoToTarget()
    {
        if (GetNavMeshAgent().pathPending) return false;
        if (GetNavMeshAgent().pathStatus != NavMeshPathStatus.PathComplete) return false;

        Log(GetNavMeshAgent().pathStatus);

        bool isClose = GetNavMeshAgent().remainingDistance <= .5f;
        GetNavMeshAgent().isStopped = isClose;
        Log($"({GetNavMeshAgent().remainingDistance} units) left ({GetNavMeshAgent().destination} <= {transform.position}");
        Log(isClose.BoolColor());
        return isClose;
    }

    #endregion

    #region Private Methods
    /// <summary>
    /// Checks if AI has set or (if has no state set) can have a current State. 
    /// </summary>
    /// <returns>True if has a current state or was disabled</returns>
    private bool HasState()
    {
        if (m_CurrentState != null) return true;
        if (m_States == null || m_States.Length == 0)
        {
            HandleNoStates();
            return true;
        }
        Log("Has no Current State!");
        return false;
    }
    /// <summary>
    /// If it's impossible to set current state, disables the AI and throws an error.
    /// </summary>
    private void HandleNoStates()
    {
        DisableAI();
        LogError($"{name} has no States!");
    }

    /// <summary>
    /// Disables the AI
    /// </summary>
    private void DisableAI()
    {
        Log("Disabling AI!".Color(Color.red));
        m_IsAwake = false;
    }

    private void HandleCurrentState()
    {
        if (!m_CurrentState.IsComplete)
        {
            Log($"Performing current state...");
            m_CurrentState.Act(this);
            return;
        }

        Log($"Changing to next state...");
        m_CurrentState.OnExit(this);
        m_CurrentState = m_CurrentState.NextState;
        if (m_CurrentState != null)
            m_CurrentState.OnEnter(this);
    }

    private NavMeshAgent GetNavMeshAgent() => m_NavMeshAgent == null ? m_NavMeshAgent = GetComponent<NavMeshAgent>() : m_NavMeshAgent;

    #endregion
}
