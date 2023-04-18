using UnityEngine;

[RequireComponent(typeof(AIScanner))]
public class AIController : EnhancedBehaviour
{
    #region Properties & Fields
    [SerializeField]
    private AIState[] m_States;

    private AIState m_CurrentState = null;

    private AIScanner m_AIScanner;

    [SerializeField]
    private bool m_IsAwake = false;

    [SerializeField]
    private bool m_ChooseStateAtRandom = true;

    [SerializeField]
    private bool m_ExitAfterState = true; //if set to true, AI after first completed state will travel to the exit; if not, will grab new state.
    #endregion

    #region Unity Methods
    private void Update()
    {
        if (!m_IsAwake) return;

        if (!HasState())
            SetCurrentState();
        TryPerformState();
    }

    #endregion

    #region Public Methods
    public AIScanner GetAIScanner() => m_AIScanner == null ? m_AIScanner = GetComponent<AIScanner>() : m_AIScanner;
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
            EmptyStates();
            return true;
        }
        return false;
    }
    /// <summary>
    /// If it's impossible to set current state, disables the AI and throws an error.
    /// </summary>
    private void EmptyStates()
    {
        DisableAI();
        LogError($"{name} has no States!");
    }

    /// <summary>
    /// Disables the AI
    /// </summary>
    private void DisableAI()
    {
        m_IsAwake = false;
    }
    /// <summary>
    /// Sets a new current state. At random (if <see cref="m_ChooseStateAtRandom"/> is true) or first on the list.
    /// </summary>
    private void SetCurrentState()
    {
        if (!m_ChooseStateAtRandom)
            m_CurrentState = m_States[0];
        else
            m_CurrentState = m_States[Random.Range(0, m_States.Length)];
    }

    private void TryPerformState()
    {
        // m_CurrentState
    }

    #endregion
}
