using UnityEngine;

namespace Assets.Scripts.AI.State
{
    public class AIState : ScriptableObject
    {
        #region Properties & Fields
        public AIState NextState = null;
        public Color Color = Color.white;
        public bool IsComplete => m_IsComplete;
        [SerializeField]
        protected bool m_IsComplete = false;
        #endregion

        #region Public methods
        public virtual void OnEnter(AIController aIController) => m_IsComplete = false;
        public virtual void OnExit(AIController aIController) => m_IsComplete = false;
        public virtual void Act(AIController controller) => m_IsComplete = true; 
        #endregion

    }
}