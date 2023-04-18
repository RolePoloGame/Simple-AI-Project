using UnityEngine;

[CreateAssetMenu(menuName = "AI/State/Fulfill Need")]
public class FulfillNeedAIState : AIState
{
    [SerializeField]
    private Need m_Need;

    public override void Act(AIController aIController)
    {
        if (m_Need == null) return;

        if (!m_Need.HasSatisfyer)
        {
            
        }
    }
}
