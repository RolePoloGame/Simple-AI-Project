using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Needs/Need Satisfyer")]
public class NeedSatisfyer : ScriptableObject
{
    [SerializeField]
    private List<NeedAction> m_NeedActions;
    private NeedAction m_CurrentAction = null;
    private bool m_IsSatified = false;

    public bool IsSatisfied() => false;

    public void Satisfy()
    {
        
    }

}
