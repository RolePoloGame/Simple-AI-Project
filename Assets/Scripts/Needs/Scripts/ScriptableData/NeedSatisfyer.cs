using Assets.Scripts.AI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Needs.Scripts.ScriptableData
{
    [CreateAssetMenu(menuName = "Needs/Need Satisfyer")]
    public class NeedSatisfyer : ScriptableObject
    {
        #region Properties & Fields
        [SerializeField]
        private List<NeedAction> m_NeedActions;
        private NeedAction m_CurrentAction = null;

        public bool IsPerformed => m_IsPerformed;
        private bool m_IsPerformed = false;

        private int m_CurrentActionIndex = 0;
        #endregion
        #region Public methods
        public void OnEnter(AIController aiController)
        {
            if (m_NeedActions == null || m_NeedActions.Count == 0) return;
            ResetActions(aiController);
        }

        public void OnExit(AIController aiController)
        {
            m_IsPerformed = false;
        }
        public void Act(AIController aiController)
        {
            HandleAction(aiController);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Calls Act on <see cref="m_CurrentAction"/>, if it's finished calls <see cref="GetNextAction(AIController)"/>. If all actions have been performed sets <see cref="m_IsPerformed"/> as true
        /// </summary>
        /// <param name="aiController"></param>
        private void HandleAction(AIController aiController)
        {
            if (m_CurrentAction == null)
            {
                m_IsPerformed = true;
                return;
            }

            m_CurrentAction.Act(aiController);

            if (!m_CurrentAction.IsPerformed) return;

            GetNextAction(aiController);
        }
        /// <summary>
        /// Increases <see cref="m_CurrentActionIndex"/> and calls <see cref="SetNewCurrentAction(AIController)". If all actions have been performed sets <see cref="m_CurrentAction"/> to null
        /// </summary>
        private void GetNextAction(AIController aiController)
        {
            m_CurrentAction.OnExit(aiController);
            m_CurrentActionIndex++;

            if (m_CurrentActionIndex >= m_NeedActions.Count)
            {
                m_CurrentAction = null;
                return;
            }

            SetNewCurrentAction(aiController);
        }
        /// <summary>
        /// Sets <see cref="m_CurrentAction"/> to an action from <see cref="m_NeedActions"/> array given by <see cref="m_CurrentActionIndex"/> index
        /// </summary>
        /// <param name="aiController"></param>
        private void SetNewCurrentAction(AIController aiController)
        {
            m_CurrentAction = m_NeedActions[m_CurrentActionIndex];
            m_CurrentAction.OnEnter(aiController);
        }
        /// <summary>
        /// Resets Current index and <see cref="m_IsPerformed"/> then sets calls <see cref="SetNewCurrentAction(AIController)"/>
        /// </summary>
        /// <param name="aiController"></param>
        private void ResetActions(AIController aiController)
        {
            m_IsPerformed = false;
            m_CurrentActionIndex = 0;
            SetNewCurrentAction(aiController);
        }
        #endregion
    }
}