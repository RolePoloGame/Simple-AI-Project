using Assets.Scripts.Needs.Scripts.ScriptableData;
using UnityEngine;

namespace Assets.Scripts.AI.State
{
    [CreateAssetMenu(menuName = "AI/State/Fulfill Need")]
    public class FulfillNeedAIState : AIState
    {
        #region Properties & Fields
        [SerializeField]
        private Need m_Need;
        #endregion

        #region Public methods
        public override void Act(AIController aiController)
        {
            HandleNeed(aiController);
        }
        /// <summary>
        ///  Travels to <see cref="Need"/>'s Target. If no given, searches for one. If arrived at target, performs action. Sets <see cref="m_IsStateComplete"/> if <see cref="Need"/> is satisfied
        /// </summary>
        private void HandleNeed(AIController aiController)
        {
            if (m_Need == null) return;

            aiController.SetGoToTarget(m_Need.GetTarget());
            m_Need.Act(aiController);
            m_IsComplete = m_Need.IsNeedSatisfied;
        }

        public override void OnEnter(AIController aiController)
        {
            if (!m_Need.HasSatisfyer)
            {
                Transform closestSatisfier = aiController.GetAIScanner().FindClosestNeedSatisfyer<NeedSatisfactionObject>(m_Need.GetSatysfiers());
                if (closestSatisfier == null) return;
                m_Need.SetSatisfier(closestSatisfier.GetComponent<NeedSatisfactionObject>(), aiController);
            }
            m_Need.OnEnter(aiController);
            m_IsComplete = false;
        }

        public override void OnExit(AIController aIController)
        {
            m_Need.OnExit(aIController);
            m_IsComplete = false;
        }
        #endregion
    }
}