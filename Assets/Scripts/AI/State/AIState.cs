using UnityEngine;

public class AIState : ScriptableObject
{
    public AIState NextState = null;
    public Color Color = Color.white;
    public bool IsComplete => m_IsStateComplete;
    [SerializeField]
    protected bool m_IsStateComplete = false;
    public virtual void OnEnter(AIController aIController) => m_IsStateComplete = false;
    public virtual void OnExit(AIController aIController) => m_IsStateComplete = false;
    public virtual void Act(AIController controller) => m_IsStateComplete = true;

}
