using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Needs/Need")]
public class Need : ScriptableObject
{
    [SerializeField]
    private NeedSatisfyer[] m_NeedSatisfyers;

    private NeedSatisfyer m_SelectedSatisfyer;

    public bool HasSatisfyer => m_SelectedSatisfyer != null;

    #region Public methods
    public NeedSatisfyer[] GetSatysfiers() => m_NeedSatisfyers
    public void Act()
    {
        if (!HasSatisfyer)
            FindSatisfyer();
    }
    #endregion

    #region Private methods
    private void FindSatisfyer()
    {
        throw new NotImplementedException();
    }

    private void NotifyCurrentState()
    {

    } 
    #endregion

}
