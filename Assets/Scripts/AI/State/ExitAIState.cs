using UnityEngine;
[CreateAssetMenu(menuName = "AI/Exit State")]
public class ExitAIState : AIState
{
    public override void Act(AIController aiController)
    {
        if (!aiController.GoToTarget()) return;
        m_IsStateComplete = true;
    }

    public override void OnEnter(AIController aIController)
    {
        aIController.SetGoToTarget(ExitDoorway.Instance.transform.position);
        m_IsStateComplete = false;
    }
}
