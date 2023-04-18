using UnityEngine;

public class AIState : ScriptableObject
{
    public bool IsComplete => m_IsStateComplete;
    private bool m_IsStateComplete = false;

    public virtual void Act(AIController controller)
    {

    }
}
