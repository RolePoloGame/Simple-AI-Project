using Assets.Scripts.AI;
using Assets.Scripts.Core;
using Assets.Scripts.Needs.Scripts.ScriptableData;
using System;
using UnityEngine;

[SelectionBase]
public class NeedSatisfactionObject : EnhancedBehaviour
{
    #region Properties & Fields
    [SerializeField]
    private NeedSatisfyer m_NeedSatisfyer;
    #endregion

    #region Public Methods
    /// <summary>
    /// Compares <see cref="NeedSatisfyer"/> with the inner one
    /// </summary>
    /// <param name="need"><see cref="NeedSatisfyer" to compare to/></param>
    /// <returns></returns>
    public bool IsSameSatysfier(NeedSatisfyer need)
    {
        if (need == null) return false;
        return m_NeedSatisfyer.Equals(need);
    }
    public void OnEnter(AIController aiController) => m_NeedSatisfyer.OnEnter(aiController);
    public void OnExit(AIController aiController) => m_NeedSatisfyer.OnExit(aiController);
    public bool Act(AIController aiController)
    {
        m_NeedSatisfyer.Act(aiController);
        if (m_NeedSatisfyer.IsPerformed)
        {
            m_NeedSatisfyer.OnExit(aiController);
            return true;
        }

        return false;
    }

    #endregion
}
