using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Needs/Need Satisfyer")]
public class NeedSatisfyer : ScriptableObject
{
    [SerializeField]
    private List<NeedAction> m_NeedActions;
    private NeedAction m_CurrentAction = null;
    public bool IsSatisfied => m_IsSatified;
    private bool m_IsSatified = false;
    private int m_CurrentActionIndex = 0;
    public void Initialize(AIController aiController)
    {
        if (m_NeedActions == null || m_NeedActions.Count == 0) return;
        m_IsSatified = false;
        m_CurrentActionIndex = 0;
        ApplyChange(aiController);
    }

    public void Act(AIController aiController)
    {
        Debug.Log("Performing Need Action...");
        if (m_CurrentAction == null)
        {
            Debug.Log("Need Satisfied...");
            m_IsSatified = true;
            return;
        }

        m_CurrentAction.Act(aiController);

        if (!m_CurrentAction.IsPerformed) return;

        GetNextAction(aiController);
    }

    private void GetNextAction(AIController aiController)
    {
        m_CurrentAction.OnExit(aiController);
        m_CurrentActionIndex++;

        if (m_CurrentActionIndex >= m_NeedActions.Count)
        {
            m_CurrentAction = null;
            return;
        }

        ApplyChange(aiController);
    }

    private void ApplyChange(AIController aiController)
    {
        m_CurrentAction = m_NeedActions[m_CurrentActionIndex];
        m_CurrentAction.OnEnter(aiController);
    }
}
