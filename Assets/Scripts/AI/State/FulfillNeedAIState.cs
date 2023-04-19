using System;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State/Fulfill Need")]
public class FulfillNeedAIState : AIState
{
    [SerializeField]
    private Need m_Need;

    public override void Act(AIController aiController)
    {
        if (m_Need == null) return;

        if (!m_Need.HasSatisfyer)
        {
            Transform closestSatisfier = aiController.GetAIScanner().FindClosestNeedSatisfyer<NeedSatisfactionObject>(m_Need.GetSatysfiers());
            if (closestSatisfier == null) return;
            m_Need.SetSatisfier(closestSatisfier.GetComponent<NeedSatisfactionObject>());
        }
        aiController.SetGoToTarget(m_Need.GetTarget());
        m_Need.Act(aiController);
        m_IsStateComplete = m_Need.IsNeedSatisfied;
    }

    public override void OnEnter(AIController aIController)
    {
        base.OnEnter(aIController);
        m_Need.OnEnter(aIController);
    }

    public override void OnExit(AIController aIController)
    {
        base.OnExit(aIController);
        m_Need.OnExit(aIController);
    }
}
