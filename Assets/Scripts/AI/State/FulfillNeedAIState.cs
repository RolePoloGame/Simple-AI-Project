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
            Debug.Log("Finding Satisfyer...");
            Transform closestSatisfier = aiController.GetAIScanner().FindClosestNeedSatisfyer<NeedSatisfactionObject>(m_Need.GetSatysfiers());
            if (closestSatisfier == null) return;
            m_Need.SetSatisfier(closestSatisfier.GetComponent<NeedSatisfactionObject>());
            m_Need.Initialize(aiController);
            aiController.SetGoToTarget(closestSatisfier.position);
        }
        Debug.Log($"{name} state running...");
        m_Need.Act(aiController);
        m_IsStateComplete = m_Need.IsNeedSatisfied;
    }
}
