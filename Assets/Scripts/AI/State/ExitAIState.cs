using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.AI.State
{
    [CreateAssetMenu(menuName = "AI/Exit State")]
    public class ExitAIState : AIState
    {
        public override void Act(AIController aiController)
        {
            if (!aiController.GoToTarget()) return;
            m_IsComplete = true;
        }

        public override void OnEnter(AIController aIController)
        {
            m_IsComplete = false;
            aIController.SetGoToTarget(ExitDoorway.Instance.transform.position);
        }

    }
}