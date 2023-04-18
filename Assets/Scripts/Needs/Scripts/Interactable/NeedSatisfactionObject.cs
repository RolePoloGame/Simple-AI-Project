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
    public void Initialize(AIController aiController) => m_NeedSatisfyer.Initialize(aiController);
    public bool Act(AIController aiController)
    {
        m_NeedSatisfyer.Act(aiController);
        return m_NeedSatisfyer.IsSatisfied;
    } 
    #endregion
}
