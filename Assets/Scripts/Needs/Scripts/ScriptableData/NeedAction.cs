using System;
using UnityEngine;

public abstract class NeedAction : ScriptableObject
{
    public bool IsPerformed => m_IsPerformed;
    protected bool m_IsPerformed;
    public virtual void Act(AIController aiController)
    {
        m_IsPerformed = true;
    }

    public abstract void OnEnter(AIController aiController);
    public virtual void OnExit(AIController aiController)
    {
        m_IsPerformed = false;
    }
}
