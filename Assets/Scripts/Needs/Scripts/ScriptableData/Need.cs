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
    public void Initialize(AIController aiController)
    {
        m_SelectedSatisfyer.Initialize(aiController);
        m_IsNeedSatisfied = false;
    }
    public void Act(AIController aiController)
    {
        if (!HasSatisfyer) return;
        if (!aiController.GoToTarget()) return;
        if (IsNeedSatisfied) return;
        Debug.Log("Attempting need fulfilling...");
        m_IsNeedSatisfied = m_SelectedSatisfyer.Act(aiController);
    }
    #endregion
}
