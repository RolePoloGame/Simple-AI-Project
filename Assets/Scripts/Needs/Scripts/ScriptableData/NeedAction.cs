using UnityEngine;

public abstract class NeedAction : ScriptableObject
{
    public bool IsPerformed => m_IsPerformed;
    private bool m_IsPerformed;
    public virtual void RunAction()
    {
        m_IsPerformed = true;
    }
}
