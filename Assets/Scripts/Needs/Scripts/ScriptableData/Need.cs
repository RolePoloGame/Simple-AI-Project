using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Needs/Need")]

public class Need : ScriptableObject
{
    [SerializeField]
    private NeedSatisfyer[] m_NeedSatisfyers;

    private NeedSatisfactionObject m_SelectedSatisfyer;

    public bool HasSatisfyer => m_SelectedSatisfyer != null;
    public bool IsNeedSatisfied => m_IsNeedSatisfied;
    [SerializeField]
    private bool m_IsNeedSatisfied = false;

    #region Public methods
    public void SetSatisfier(NeedSatisfactionObject nso) => m_SelectedSatisfyer = nso;
    public NeedSatisfyer[] GetSatysfiers() => m_NeedSatisfyers;
    public void OnEnter(AIController aiController)
    {
        m_SelectedSatisfyer.OnEnter(aiController);
        m_IsNeedSatisfied = false;
    }

    public void OnExit(AIController aiController)
    {
        m_SelectedSatisfyer.OnExit(aiController);
        m_IsNeedSatisfied = false;
    }

    public void Act(AIController aiController)
    {
        if (!HasSatisfyer) return;
        if (!aiController.GoToTarget()) return;
        if (IsNeedSatisfied) return;
        m_IsNeedSatisfied = m_SelectedSatisfyer.Act(aiController);
    }

    public Vector3 GetTarget()
    {
        return m_SelectedSatisfyer.transform.position;
    }
    #endregion
}
